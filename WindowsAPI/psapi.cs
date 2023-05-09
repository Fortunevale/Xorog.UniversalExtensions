using System.Runtime.InteropServices;

namespace Xorog.UniversalExtensions.WindowsAPI;

#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
public class psapi
{
    [DllImport("psapi.dll")]
    public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In][MarshalAs(UnmanagedType.U4)] int nSize);
}
