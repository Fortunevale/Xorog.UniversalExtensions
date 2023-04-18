using System.Runtime.InteropServices;

namespace Xorog.UniversalExtensions.WindowsAPI;

public class kernel32
{
    /// <summary>
    /// Opens an existing local process object.
    /// </summary>
    /// <param name="processAccess">The access to the process object. This access right is checked against the security descriptor for the process. This parameter can be one or more of the process access rights.</param>
    /// <param name="bInheritHandle">If this value is TRUE, processes created by this process will inherit the handle. Otherwise, the processes do not inherit this handle.</param>
    /// <param name="processId">The identifier of the local process to be opened.</param>
    /// <returns>If the function succeeds, the return value is an open handle to the specified process. If the function fails, the return value is NULL.</returns>
    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

    /// <summary>
    /// Closes an open object handle.
    /// </summary>
    /// <param name="hObject">A valid handle to an open object.</param>
    /// <returns>If the function succeeds, the return value is true.</returns>
    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool CloseHandle(IntPtr hObject);
}
