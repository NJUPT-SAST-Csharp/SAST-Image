using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace App.Native;

internal static partial class NativeMethods
{
    private const string DllName = "Sast";

    [LibraryImport(DllName, EntryPoint = "init", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr Init(string title, uint width, uint height, string resourceDir);

    [LibraryImport(DllName, EntryPoint = "run")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int Run();

    [LibraryImport(DllName, EntryPoint = "quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void Quit();

    [LibraryImport(DllName, EntryPoint = "free_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void FreeString(IntPtr ptr);

    [LibraryImport(DllName, EntryPoint = "version")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr Version();
}

public sealed class NativeStringHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public NativeStringHandle()
        : base(ownsHandle: true)
    {
    }

    public NativeStringHandle(IntPtr handle)
        : base(ownsHandle: true)
    {
        SetHandle(handle);
    }

    public string? ToManagedString()
    {
        return IsInvalid
            ? null
            : Marshal.PtrToStringUTF8(handle);
    }

    protected override bool ReleaseHandle()
    {
        NativeMethods.FreeString(handle);
        return true;
    }
}

public static class AppNative
{
    public static void Init(
        string title,
        uint width,
        uint height,
        string resourceDir)
    {
        using var error = new NativeStringHandle(
            NativeMethods.Init(title, width, height, resourceDir));

        var message = error.ToManagedString();

        if (message is not null)
            throw new NativeException(message);
    }

    public static void Run()
    {
        var exitCode = NativeMethods.Run();

        if (exitCode != 0)
            throw new NativeException($"Native event loop exited with code {exitCode}.");
    }

    public static int RunRaw()
    {
        return NativeMethods.Run();
    }

    public static void Quit()
    {
        NativeMethods.Quit();
    }

    public static string Version()
    {
        using var value = new NativeStringHandle(NativeMethods.Version());

        return value.ToManagedString() ?? string.Empty;
    }
}

public sealed class NativeException(string message) : Exception(message);
