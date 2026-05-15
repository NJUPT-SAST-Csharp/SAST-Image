using App.Framework.Interop;

namespace App.Framework;

/// <summary>Batches dirty components and flushes re-renders on demand via RAF.</summary>
internal static partial class Scheduler
{
    private static readonly HashSet<ComponentInstance> s_dirty = new();

    public static void Enqueue(ComponentInstance comp)
    {
        bool first;
        lock (s_dirty)
        {
            first = s_dirty.Count == 0;
            s_dirty.Add(comp);
        }

        // Only schedule a flush when the queue transitions from empty → non-empty.
        if (first)
            DomInterop.ScheduleFlush();
    }

    [System.Runtime.InteropServices.JavaScript.JSExport]
    public static void FlushRenderQueue()
    {
        ComponentInstance[] batch;
        lock (s_dirty)
        {
            batch = s_dirty.ToArray();
            s_dirty.Clear();
        }

        foreach (var comp in batch)
        {
            if (comp.IsMounted && comp.ParentDom != null)
                comp.ExecuteRender(comp.ParentDom);
        }
    }
}
