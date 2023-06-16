using System.Reflection;

namespace Xorog.UniversalExtensions;

public static class UniversalExtensions
{
    /// <summary>
    /// Attaches a logger to UniversalExtensions. Used for Debugging.
    /// </summary>
    /// <param name="logger"></param>
    public static void AttachLogger(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Loads all referenced dependencies in an <see cref="AppDomain"/>.
    /// <para>Prevents <see cref="FileNotFoundException"/>s that are caused by utilizing not yet loaded assemblies after the system has been updated.</para>
    /// </summary>
    /// <param name="domain">The <see cref="AppDomain"/> to load all dependencies from.</param>
    public static void LoadAllReferencedAssemblies(this AppDomain domain)
    {
        _logger?.LogDebug("Loading all assemblies..");

        var assemblyCount = 0;
        foreach (Assembly assembly in domain.GetAssemblies())
        {
            LoadReferencedAssembly(assembly);
        }

        void LoadReferencedAssembly(Assembly assembly)
        {
            try
            {
                foreach (AssemblyName name in assembly.GetReferencedAssemblies())
                {
                    if (!AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName == name.FullName))
                    {
                        assemblyCount++;
                        _logger?.LogDebug("Loading {Name}..", name.Name);
                        LoadReferencedAssembly(Assembly.Load(name));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError("Failed to load an assembly", ex);
            }
        }

        _logger?.LogInformation("Loaded {assemblyCount} assemblies.", assemblyCount);
    }

    /// <summary>
    /// Adds additional data to an exception.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="exception"></param>
    /// <param name="Key"></param>
    /// <param name="Data"></param>
    /// <returns></returns>
    public static T AttachData<T>(this T exception, string Key, object? Data) where T : Exception
    {
        exception.Data.Add(Key, Data);
        return exception;
    }

    /// <summary>
    /// Get the current CPU Usage on all platforms
    /// </summary>
    /// <returns></returns>
    public static async Task<double> GetCpuUsageForProcess()
    {
        var startTime = DateTime.UtcNow;
        var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
        await Task.Delay(500);

        var endTime = DateTime.UtcNow;
        var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
        var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
        var totalMsPassed = (endTime - startTime).TotalMilliseconds;
        var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        return cpuUsageTotal * 100;
    }
}