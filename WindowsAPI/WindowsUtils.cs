using System.Runtime.CompilerServices;

namespace Xorog.UniversalExtensions.WindowsAPI;

public class WindowsUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static FileInfo? GetMainModuleFilepath(int pid)
    {
        var processHandle = kernel32.OpenProcess(0x0400 | 0x0010, false, pid);

        if (processHandle == IntPtr.Zero)
        {
            return null;
        }

        const int lengthSb = 4000;

        var sb = new StringBuilder(lengthSb);

        string? result = null;

        if (psapi.GetModuleFileNameEx(processHandle, IntPtr.Zero, sb, lengthSb) > 0)
        {
            result = Path.GetFullPath(sb.ToString());
        }

        kernel32.CloseHandle(processHandle);

        if (result == null)
            return null;

        return new FileInfo(result);
    }
}
