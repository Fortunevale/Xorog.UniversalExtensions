using System.Runtime.InteropServices;

namespace Xorog.UniversalExtensions.WindowsAPI;

public class psapi
{
    [DllImport("psapi.dll")]
    public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In][MarshalAs(UnmanagedType.U4)] int nSize);
}
