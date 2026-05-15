use std::borrow::Cow;
use std::ffi::{c_char, c_int, CStr, CString};
use std::path::PathBuf;
use std::sync::OnceLock;

use tao::{
    event::{Event, WindowEvent},
    event_loop::{ControlFlow, EventLoopBuilder, EventLoopProxy},
    window::WindowBuilder,
};
use wry::{
    http::{header::CONTENT_TYPE, Request, Response},
    WebViewBuilder,
};

// ── Global state ────────────────────────────────────────────────────

struct WindowConfig {
    title: String,
    width: f64,
    height: f64,
    resource_dir: PathBuf,
}

static CONFIG: OnceLock<WindowConfig> = OnceLock::new();
static PROXY: OnceLock<EventLoopProxy<UserEvent>> = OnceLock::new();

enum UserEvent {
    Quit,
}

// ── Helpers ─────────────────────────────────────────────────────────

unsafe fn cstr_to_str<'a>(ptr: *const c_char) -> &'a str {
    if ptr.is_null() {
        return "";
    }
    unsafe { CStr::from_ptr(ptr) }.to_str().unwrap_or("")
}

fn error_ptr(msg: &str) -> *mut c_char {
    CString::new(msg).unwrap_or_default().into_raw()
}

fn guess_mime(path: &str) -> &'static str {
    if path.ends_with(".html") || path == "/" || path.is_empty() {
        "text/html"
    } else if path.ends_with(".css") {
        "text/css"
    } else if path.ends_with(".js") || path.ends_with(".mjs") {
        "text/javascript"
    } else if path.ends_with(".json") {
        "application/json"
    } else if path.ends_with(".png") {
        "image/png"
    } else if path.ends_with(".jpg") || path.ends_with(".jpeg") {
        "image/jpeg"
    } else if path.ends_with(".svg") {
        "image/svg+xml"
    } else if path.ends_with(".wasm") {
        "application/wasm"
    } else if path.ends_with(".woff2") {
        "font/woff2"
    } else if path.ends_with(".woff") {
        "font/woff"
    } else {
        "application/octet-stream"
    }
}

// ── Custom protocol handler ─────────────────────────────────────────

fn handle_protocol(
    _scheme: &str,
    request: Request<Vec<u8>>,
) -> wry::Result<Response<Cow<'static, [u8]>>> {
    let root = CONFIG
        .get()
        .map(|c| c.resource_dir.clone())
        .unwrap_or_else(|| PathBuf::from("."));

    let path = request.uri().path();
    let relative = if path == "/" { "index.html" } else { &path[1..] };

    let full_path = match std::fs::canonicalize(root.join(relative)) {
        Ok(p) => p,
        Err(_) => {
            return Ok(Response::builder()
                .status(404)
                .body(Cow::Owned(b"Not Found".to_vec()))
                .unwrap());
        }
    };

    // Basic directory-traversal guard.
    if !full_path.starts_with(
        &*root
            .canonicalize()
            .unwrap_or_else(|_| root.clone()),
    ) {
        return Ok(Response::builder()
            .status(403)
            .body(Cow::Owned(b"Forbidden".to_vec()))
            .unwrap());
    }

    let content = std::fs::read(&full_path)?;
    let mime = guess_mime(relative);

    Ok(Response::builder()
        .header(CONTENT_TYPE, mime)
        .body(Cow::Owned(content))
        .unwrap())
}

fn handle_protocol_wrapper(
    scheme: &str,
    request: Request<Vec<u8>>,
) -> Response<Cow<'static, [u8]>> {
    match handle_protocol(scheme, request) {
        Ok(r) => r,
        Err(e) => Response::builder()
            .header(CONTENT_TYPE, "text/plain")
            .status(500)
            .body(Cow::Owned(e.to_string().as_bytes().to_vec()))
            .unwrap(),
    }
}

// ── FFI ─────────────────────────────────────────────────────────────

/// Initialize the WebView window.
///
/// `title`  — window title (UTF-8, null-terminated).
/// `width`  — window width in logical pixels.
/// `height` — window height in logical pixels.
/// `resource_dir` — absolute path to the directory containing static assets
///                   (index.html, JS, CSS, etc.), served via `wry://localhost`.
///
/// Returns null on success, or a heap-allocated error string on failure.
/// The caller must free error strings with `free_string`.
#[unsafe(no_mangle)]
pub extern "C" fn init(
    title: *const c_char,
    width: u32,
    height: u32,
    resource_dir: *const c_char,
) -> *mut c_char {
    let title_str = unsafe { cstr_to_str(title) }.to_owned();
    let dir = unsafe { cstr_to_str(resource_dir) };

    let resource_dir = match std::fs::canonicalize(dir) {
        Ok(p) => p,
        Err(e) => return error_ptr(&format!("resource_dir not found: {e}")),
    };

    if CONFIG
        .set(WindowConfig {
            title: title_str,
            width: width as f64,
            height: height as f64,
            resource_dir,
        })
        .is_err()
    {
        return error_ptr("init already called");
    }

    std::ptr::null_mut()
}

/// Run the event loop. Blocks the calling thread until the window is closed.
///
/// Must be called after `init`. Returns 0 on clean exit.
#[unsafe(no_mangle)]
pub extern "C" fn run() -> c_int {
    let config = match CONFIG.get() {
        Some(c) => c,
        None => return 1,
    };

    let event_loop = EventLoopBuilder::<UserEvent>::with_user_event().build();

    let proxy = event_loop.create_proxy();
    let _ = PROXY.set(proxy);

    let window = match WindowBuilder::new()
        .with_title(&config.title)
        .with_inner_size(tao::dpi::LogicalSize::new(config.width, config.height))
        .build(&event_loop)
    {
        Ok(w) => w,
        Err(_) => return 1,
    };

    let builder = WebViewBuilder::new()
        .with_custom_protocol("wry".into(), handle_protocol_wrapper)
        .with_url("wry://localhost");

    #[cfg(any(
        target_os = "windows",
        target_os = "macos",
        target_os = "ios",
        target_os = "android"
    ))]
    let _webview = {
        let wv = builder.build(&window);
        match wv {
            Ok(w) => w,
            Err(_) => return 1,
        }
    };

    #[cfg(not(any(
        target_os = "windows",
        target_os = "macos",
        target_os = "ios",
        target_os = "android"
    )))]
    let _webview = {
        use tao::platform::unix::WindowExtUnix;
        use wry::WebViewBuilderExtUnix;

        let vbox = match window.default_vbox() {
            Some(v) => v,
            None => return 1,
        };

        match builder.build_gtk(vbox) {
            Ok(w) => w,
            Err(_) => return 1,
        }
    };

    event_loop.run(move |event, _target, control_flow| {
        *control_flow = ControlFlow::Wait;

        match event {
            Event::WindowEvent {
                event: WindowEvent::CloseRequested,
                ..
            } => *control_flow = ControlFlow::Exit,

            Event::UserEvent(UserEvent::Quit) => {
                *control_flow = ControlFlow::Exit;
            }

            _ => {}
        }
    })
}

/// Post a quit message. Safe to call from any thread after `run` has started.
#[unsafe(no_mangle)]
pub extern "C" fn quit() {
    if let Some(proxy) = PROXY.get() {
        let _ = proxy.send_event(UserEvent::Quit);
    }
}

/// Free a string previously returned by this library.
#[unsafe(no_mangle)]
pub extern "C" fn free_string(ptr: *mut c_char) {
    if ptr.is_null() {
        return;
    }

    unsafe {
        let _ = CString::from_raw(ptr);
    }
}

/// Get the platform library version.
///
/// Returns a heap-allocated string.
/// The caller must free it with `free_string`.
#[unsafe(no_mangle)]
pub extern "C" fn version() -> *mut c_char {
    CString::new(env!("CARGO_PKG_VERSION"))
        .unwrap_or_default()
        .into_raw()
}
