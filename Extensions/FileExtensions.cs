namespace Xorog.UniversalExtensions;

public static class FileExtensions
{
    /// <summary>
    /// Copy a directory recursively
    /// </summary>
    /// <param name="sourceDirName"></param>
    /// <param name="destDir"></param>
    /// <param name="copySubDirs"></param>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public static void CopyRecursively(this DirectoryInfo sourceDir, DirectoryInfo destDir, bool copySubDirs = true)
    {
        if (!sourceDir.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDir.FullName}")
                .AttachData("sourceDir", sourceDir)
                .AttachData("destDir", destDir)
                .AttachData("copySubDirs", copySubDirs);
        }

        DirectoryInfo[] dirs = sourceDir.GetDirectories();

        destDir.Create();

        FileInfo[] files = sourceDir.GetFiles();
        foreach (FileInfo file in files)
        {
            var newFilePath = Path.Combine(destDir.FullName, file.Name);

            _logger?.LogDebug("Copying '{file}' to '{dest}'", file.FullName, newFilePath);
            file.CopyTo(newFilePath, false);
        }

        if (copySubDirs)
            foreach (DirectoryInfo subdir in dirs)
                CopyRecursively(subdir, new DirectoryInfo(Path.Combine(destDir.FullName, subdir.Name)), copySubDirs);
    }

    /// <summary>
    /// Try deleting the given files and directories until able to or reaching maximum retry count.
    /// </summary>
    /// <param name="directoryPaths">A list of directories to clean up</param>
    /// <param name="filePaths">A list of files to clean up</param>
    /// <param name="maxRetryCount">The maximum amount of retries</param>
    /// <returns></returns>
    public static async Task CleanupFilesAndDirectories(List<string>? directoryPaths, List<string>? filePaths, int maxRetryCount = 100)
    {
        var failCount = 0;

        List<Exception> exceptions = new();

        if (directoryPaths?.IsNotNullAndNotEmpty() ?? false)
            foreach (string DirectoryPath in directoryPaths)
            {
                while (Directory.Exists(DirectoryPath))
                {
                    try
                    {
                        Directory.Delete(DirectoryPath, true);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                        await Task.Delay(5000);
                        failCount++;

                        if (failCount > maxRetryCount)
                            throw new AggregateException("Failed to delete directory", exceptions);
                    }
                }
            }

        failCount = 0;
        exceptions.Clear();

        if (filePaths?.IsNotNullAndNotEmpty() ?? false)
            foreach (string file in filePaths)
            {
                while (File.Exists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                        await Task.Delay(5000);
                        failCount++;

                        if (failCount > maxRetryCount)
                            throw new AggregateException("Failed to delete file", exceptions);
                    }
                }
            }
    }
}
