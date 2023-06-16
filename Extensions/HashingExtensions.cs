namespace Xorog.UniversalExtensions;

public static class HashingExtensions
{
    /// <summary>
    /// Compute the SHA256-Hash for the given string
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ComputeSHA256Hash(string str)
    {
        using SHA256 _SHA256 = SHA256.Create();
        return BitConverter.ToString(_SHA256.ComputeHash(Encoding.ASCII.GetBytes(str))).Replace("-", "").ToLowerInvariant();
    }

    /// <summary>
    /// Compute the SHA256-Hash for a given file
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string ComputeSHA256Hash(FileInfo filePath)
    {
        using SHA256 _SHA256 = SHA256.Create();
        using FileStream fileStream = filePath.OpenRead();
        return BitConverter.ToString(_SHA256.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
    }
}
