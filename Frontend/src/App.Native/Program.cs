using System.Reflection;
using App.Native;

var webroot = Path.Combine(AppContext.BaseDirectory, "wwwroot");

var thread = new Thread(() =>
{
    AppNative.Init("SAST Image", 1280, 720, webroot);
    AppNative.Run();
});
if (OperatingSystem.IsWindows())
    thread.SetApartmentState(ApartmentState.STA);
thread.Start();
thread.Join();
