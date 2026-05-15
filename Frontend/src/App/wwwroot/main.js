import { dotnet } from './_framework/dotnet.js';

const {
  setModuleImports,
  getAssemblyExports,
  runMain,
} = await dotnet.create();

const eventStore = new WeakMap();

function setEvent(el, type, handler) {
  let events = eventStore.get(el);

  if (!events) {
    events = new Map();
    eventStore.set(el, events);
  }

  const oldHandler = events.get(type);

  if (oldHandler) {
    el.removeEventListener(type, oldHandler);
    events.delete(type);
  }

  if (handler) {
    el.addEventListener(type, handler);
    events.set(type, handler);
  }
}

setModuleImports('main.js', {
  createElement: tag => document.createElement(tag),

  createTextNode: text => document.createTextNode(text),

  appendChild(parent, child) {
    parent.appendChild(child);
    return child;
  },

  removeChild(parent, child) {
    if (child?.parentNode === parent) {
      parent.removeChild(child);
    }
    return child;
  },

  replaceChild(parent, newChild, oldChild) {
    parent.replaceChild(newChild, oldChild);
    return oldChild;
  },

  insertBefore(parent, child, ref) {
    parent.insertBefore(child, ref ?? null);
    return child;
  },

  setAttribute(el, name, value) {
    if (value == null || value === false) {
      el.removeAttribute(name);
    } else {
      el.setAttribute(name, String(value));
    }
  },

  removeAttribute(el, name) {
    el.removeAttribute(name);
  },

  setTextContent(el, text) {
    el.textContent = text ?? '';
  },

  addEventListener(el, type, handler) {
    el.addEventListener(type, handler);
  },

  removeEventListener(el, type, handler) {
    el.removeEventListener(type, handler);
  },

  setEvent,

  localStorageGet: key => localStorage.getItem(key),

  localStorageSet: (key, value) => localStorage.setItem(key, value),

  localStorageRemove: key => localStorage.removeItem(key),

  createObjectURL: (bytes, mimeType) => URL.createObjectURL(new Blob([bytes], { type: mimeType })),

  revokeObjectURL: url => URL.revokeObjectURL(url),

  querySelector: selector => document.querySelector(selector),

  querySelectorAll: selector => Array.from(document.querySelectorAll(selector)),

  setTimeout: (callback, ms) => globalThis.setTimeout(callback, ms),

  clearTimeout: id => globalThis.clearTimeout(id),

  requestAnimationFrame: callback => globalThis.requestAnimationFrame(callback),

  cancelAnimationFrame: id => globalThis.cancelAnimationFrame(id),

  scheduleFlush,
});

const exports = await getAssemblyExports('App.dll');

const flushRenderQueue = exports.App.Framework.Scheduler.FlushRenderQueue;

let scheduled = false;

function scheduleFlush() {
  if (!scheduled) {
    scheduled = true;
    requestAnimationFrame(() => {
      scheduled = false;
      flushRenderQueue();
    });
  }
}

await runMain();
