using log4net;
using System.IO;

namespace TourPlanner.Infrastructure;

public static class LoggerHelper
{
    private static bool _isInitialized;

    public static void Initialize(string configPath)
    {
        if (_isInitialized)
            return;

        if (!File.Exists(configPath))
            throw new FileNotFoundException("Logging config not found.", configPath);

        log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
        _isInitialized = true;
    }

    public static ILog GetLogger(Type type) => LogManager.GetLogger(type);
}