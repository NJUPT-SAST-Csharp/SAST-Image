using System.Runtime.InteropServices.JavaScript;
using App.Framework.Interop;

namespace App.Framework;

/// <summary>Recursive VNode-to-DOM reconciler via DomInterop.</summary>
internal static class Renderer
{
    // Keep delegates alive, JSImport marshalling may not prevent GC.
    private static readonly Dictionary<(JSObject Element, string Type), Delegate> s_events = [];

    /// <summary>Reconcile oldNode with newNode, mutating DOM under parentDom.</summary>
    public static VNode? Reconcile(
        VNode? oldNode,
        VNode? newNode,
        JSObject parentDom,
        ComponentInstance parentComp)
    {
        // Remove
        if (newNode == null && oldNode != null)
        {
            RemoveNode(oldNode, parentDom);
            return null;
        }

        // Create
        if (oldNode == null && newNode != null)
        {
            return CreateNode(newNode, parentDom, null, parentComp);
        }

        // Both null
        if (oldNode == null || newNode == null)
            return null;

        // Type mismatch — replace
        if (oldNode.GetType() != newNode.GetType())
        {
            RemoveNode(oldNode, parentDom);
            return CreateNode(newNode, parentDom, null, parentComp);
        }

        // Same type — update in place
        return newNode switch
        {
            VElement newEl => ReconcileElement((VElement)oldNode, newEl, parentDom, parentComp),
            VText newText => ReconcileText((VText)oldNode, newText),
            VComponent newComp => ReconcileComponent((VComponent)oldNode, newComp, parentDom, parentComp),
            VFragment newFrag => ReconcileFragment((VFragment)oldNode, newFrag, parentDom, parentComp),
            VContextProvider newProv => ReconcileProvider((VContextProvider)oldNode, newProv, parentDom, parentComp),
            _ => throw new InvalidOperationException($"Unknown VNode type: {newNode.GetType()}")
        };
    }

    private static VNode CreateNode(
        VNode node, JSObject parentDom, JSObject? before, ComponentInstance parentComp)
    {
        if (node is null) return null!;
        switch (node)
        {
            case VElement el:
                var dom = DomInterop.CreateElement(el.Tag);
                ApplyProps(el, dom);
                foreach (var child in el.Children)
                    CreateNode(child, dom, null, parentComp);
                DomInterop.InsertBefore(parentDom, dom, before);
                el.DomNode = dom;
                return el;

            case VText text:
                var textDom = DomInterop.CreateTextNode(text.Content);
                DomInterop.InsertBefore(parentDom, textDom, before);
                text.DomNode = textDom;
                return text;

            case VComponent comp:
                return MountComponent(comp, parentDom, parentComp);

            case VFragment frag:
                foreach (var child in frag.Children)
                    CreateNode(child, parentDom, before, parentComp);
                return frag;

            case VContextProvider prov:
                // Push context override, create child
                var saved = parentComp.ContextOverrides.GetValueOrDefault(prov.ContextType);
                parentComp.ContextOverrides[prov.ContextType] = prov.Value;
                try
                {
                    CreateNode(prov.Child, parentDom, before, parentComp);
                }
                finally
                {
                    if (saved != null)
                        parentComp.ContextOverrides[prov.ContextType] = saved;
                    else
                        parentComp.ContextOverrides.Remove(prov.ContextType);
                }
                return prov;

            default:
                throw new InvalidOperationException($"Unknown VNode type: {node.GetType()}");
        }
    }

    private static void RemoveNode(VNode node, JSObject parentDom)
    {
        if (node is null) return;
        switch (node)
        {
            case VElement el:
                if (el.DomNode != null)
                {
                    foreach (var child in el.Children)
                        RemoveNode(child, el.DomNode);
                    // Release event delegates
                    var keys = s_events.Keys.Where(k => k.Element == el.DomNode).ToList();
                    foreach (var k in keys) s_events.Remove(k);
                    DomInterop.RemoveChild(parentDom, el.DomNode);
                }
                break;

            case VText text:
                if (text.DomNode != null)
                    DomInterop.RemoveChild(parentDom, text.DomNode);
                break;

            case VComponent comp:
                // The component's inner VNode has the dom node — remove it.
                if (comp.DomNode != null)
                    DomInterop.RemoveChild(parentDom, comp.DomNode);
                break;

            case VFragment frag:
                foreach (var child in frag.Children)
                    RemoveNode(child, parentDom);
                break;

            case VContextProvider prov:
                RemoveNode(prov.Child, parentDom);
                break;
        }
    }

    // ── Element ──────────────────────────────────────────────────────

    private static VElement ReconcileElement(
        VElement old, VElement el, JSObject parentDom, ComponentInstance parentComp)
    {
        if (old.Tag != el.Tag)
        {
            // Tag changed — full replacement
            var newDom = DomInterop.CreateElement(el.Tag);
            ApplyProps(el, newDom);
            foreach (var child in el.Children)
                CreateNode(child, newDom, null, parentComp);
            DomInterop.ReplaceChild(parentDom, newDom, old.DomNode!);
            el.DomNode = newDom;
            return el;
        }

        // Same tag — update in place
        el.DomNode = old.DomNode;
        UpdateProps(old.Props, el.Props, el.DomNode!);

        // Reconcile children by index (no keys for element children)
        var ownDom = el.DomNode!;
        int maxLen = Math.Max(old.Children.Count, el.Children.Count);
        for (int i = 0; i < maxLen; i++)
        {
            var oldChild = i < old.Children.Count ? old.Children[i] : null;
            var newChild = i < el.Children.Count ? el.Children[i] : null;

            // Find the DOM node to insert before
            JSObject? before = null;
            if (newChild != null && i + 1 < maxLen)
            {
                // Try to find the next sibling's DOM node
                for (int j = i + 1; j < el.Children.Count; j++)
                {
                    var domRef = GetDomNode(el.Children[j]);
                    if (domRef != null) { before = domRef; break; }
                }
                // Also check old children for remaining DOM nodes
                if (before == null)
                {
                    for (int j = i + 1; j < old.Children.Count; j++)
                    {
                        var domRef = GetDomNode(old.Children[j]);
                        if (domRef != null) { before = domRef; break; }
                    }
                }
            }

            ReconcileWithInsert(oldChild, newChild, ownDom, parentComp, before);
        }

        return el;
    }

    private static void ReconcileWithInsert(
        VNode? oldNode, VNode? newNode, JSObject parentDom, ComponentInstance parentComp, JSObject? before)
    {
        // Same logic as Reconcile, but handles the before reference for CreateNode
        if (newNode == null && oldNode != null)
        {
            RemoveNode(oldNode, parentDom);
            return;
        }

        if (oldNode == null && newNode != null)
        {
            CreateNode(newNode, parentDom, before, parentComp);
            return;
        }

        if (oldNode == null || newNode == null) return;

        if (oldNode.GetType() != newNode.GetType())
        {
            RemoveNode(oldNode, parentDom);
            CreateNode(newNode, parentDom, before, parentComp);
            return;
        }

        // Same type — delegate to typed reconcilers (before is only needed for creation)
        switch (newNode)
        {
            case VElement newEl:
                ReconcileElement((VElement)oldNode, newEl, parentDom, parentComp);
                break;
            case VText newText:
                ReconcileText((VText)oldNode, newText);
                break;
            case VComponent newComp:
                ReconcileComponent((VComponent)oldNode, newComp, parentDom, parentComp);
                break;
            case VFragment newFrag:
                ReconcileFragment((VFragment)oldNode, newFrag, parentDom, parentComp);
                break;
            case VContextProvider newProv:
                ReconcileProvider((VContextProvider)oldNode, newProv, parentDom, parentComp);
                break;
        }
    }

    private static JSObject? GetDomNode(VNode node)
    {
        return node switch
        {
            VElement el => el.DomNode,
            VText text => text.DomNode,
            VComponent comp => comp.DomNode,
            _ => null
        };
    }

    // ── Text ─────────────────────────────────────────────────────────

    private static VText ReconcileText(VText old, VText text)
    {
        if (old.Content != text.Content)
            DomInterop.SetTextContent(old.DomNode!, text.Content);
        text.DomNode = old.DomNode;
        return text;
    }

    // ── Component ────────────────────────────────────────────────────

    private static VNode ReconcileComponent(
        VComponent old, VComponent comp, JSObject parentDom, ComponentInstance parentComp)
    {
        var instance = GetOrCreateInstance(old, comp, parentComp);

        // Inherit context from parent.
        instance.ContextOverrides.Clear();
        foreach (var (k, v) in parentComp.ContextOverrides)
            instance.ContextOverrides[k] = v;

        parentComp.MarkChildUsed(comp.RenderFn.GetHashCode(), comp.Key);
        instance.ExecuteRender(parentDom);

        // The component's rendered output DOM node becomes the VComponent's DomNode.
        comp.DomNode = instance.OldChildVNode is VElement e ? e.DomNode
            : instance.OldChildVNode is VText t ? t.DomNode
            : null;

        return comp;
    }

    private static ComponentInstance GetOrCreateInstance(
        VComponent old, VComponent comp, ComponentInstance parentComp)
    {
        var typeHash = comp.RenderFn.Method.GetHashCode();
        var key = comp.Key;

        if (parentComp.ChildInstances.TryGetValue((typeHash, key), out var existing))
        {
            existing.Parent = parentComp;
            return existing;
        }

        var instance = new ComponentInstance(comp.RenderFn, key, parentComp);
        parentComp.ChildInstances[(typeHash, key)] = instance;
        return instance;
    }

    private static VNode MountComponent(
        VComponent comp, JSObject parentDom, ComponentInstance parentComp)
    {
        var instance = new ComponentInstance(comp.RenderFn, comp.Key, parentComp);
        parentComp.ChildInstances[(comp.RenderFn.GetHashCode(), comp.Key)] = instance;
        parentComp.MarkChildUsed(comp.RenderFn.GetHashCode(), comp.Key);

        // Inherit context.
        foreach (var (k, v) in parentComp.ContextOverrides)
            instance.ContextOverrides[k] = v;

        instance.ExecuteRender(parentDom);

        comp.DomNode = instance.OldChildVNode is VElement e ? e.DomNode
            : instance.OldChildVNode is VText t ? t.DomNode
            : null;

        return comp;
    }

    // ── Fragment ─────────────────────────────────────────────────────

    private static VFragment ReconcileFragment(
        VFragment old, VFragment frag, JSObject parentDom, ComponentInstance parentComp)
    {
        int maxLen = Math.Max(old.Children.Count, frag.Children.Count);
        for (int i = 0; i < maxLen; i++)
        {
            var oldChild = i < old.Children.Count ? old.Children[i] : null;
            var newChild = i < frag.Children.Count ? frag.Children[i] : null;
            Reconcile(oldChild, newChild, parentDom, parentComp);
        }
        return frag;
    }

    // ── Context provider ─────────────────────────────────────────────

    private static VContextProvider ReconcileProvider(
        VContextProvider old, VContextProvider prov, JSObject parentDom, ComponentInstance parentComp)
    {
        var saved = parentComp.ContextOverrides.GetValueOrDefault(prov.ContextType);
        parentComp.ContextOverrides[prov.ContextType] = prov.Value;
        try
        {
            Reconcile(old.Child, prov.Child, parentDom, parentComp);
        }
        finally
        {
            if (saved != null)
                parentComp.ContextOverrides[prov.ContextType] = saved;
            else
                parentComp.ContextOverrides.Remove(prov.ContextType);
        }
        return prov;
    }

    // ── Props helpers ────────────────────────────────────────────────

    private static void ApplyProps(VElement el, JSObject dom)
    {
        foreach (var (name, value) in el.Props)
            ApplyProp(dom, name, null, value);
    }

    private static void UpdateProps(
        Dictionary<string, object?> oldProps,
        Dictionary<string, object?> newProps,
        JSObject dom)
    {
        // Remove old props not in new set.
        foreach (var name in oldProps.Keys)
        {
            if (!newProps.ContainsKey(name))
                RemoveProp(dom, name);
        }

        // Add or update props.
        foreach (var (name, value) in newProps)
        {
            oldProps.TryGetValue(name, out var oldValue);
            if (!Equals(oldValue, value))
                ApplyProp(dom, name, oldValue, value);
        }
    }

    private static void ApplyProp(JSObject dom, string name, object? oldValue, object? value)
    {
        if (name.StartsWith("on"))
        {
            var eventType = name[2..].ToLowerInvariant();
            if (value is Action handler)
            {
                Action<JSObject> wrapped = _ => handler();
                s_events[(dom, eventType)] = wrapped;
                DomInterop.SetEvent(dom, eventType, wrapped);
            }
            else if (value is Action<JSObject> typedHandler)
            {
                s_events[(dom, eventType)] = typedHandler;
                DomInterop.SetEvent(dom, eventType, typedHandler);
            }
            return;
        }

        switch (name)
        {
            case "class" or "className":
                if (value is string s)
                    DomInterop.SetAttribute(dom, "class", s);
                break;
            case "style":
                if (value is string styleStr)
                    DomInterop.SetAttribute(dom, "style", styleStr);
                break;
            default:
                if (value is string strVal)
                    DomInterop.SetAttribute(dom, name, strVal);
                else if (value == null)
                    DomInterop.RemoveAttribute(dom, name);
                else
                    DomInterop.SetAttribute(dom, name, value.ToString()!);
                break;
        }
    }

    private static void RemoveProp(JSObject dom, string name)
    {
        if (name.StartsWith("on"))
        {
            var eventType = name[2..].ToLowerInvariant();
            s_events.Remove((dom, eventType));
            DomInterop.SetEvent(dom, eventType, null!);
        }
        else
        {
            DomInterop.RemoveAttribute(dom, name);
        }
    }
}
