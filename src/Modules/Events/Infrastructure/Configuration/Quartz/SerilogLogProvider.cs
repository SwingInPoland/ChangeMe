using Quartz.Logging;
using Serilog;

namespace ChangeMe.Modules.Events.Infrastructure.Configuration.Quartz;

internal class SerilogLogProvider : ILogProvider
{
    private readonly ILogger _logger;

    internal SerilogLogProvider(ILogger logger)
    {
        _logger = logger;
    }

    public Logger GetLogger(string name) => (level, func, exception, parameters) =>
    {
        if (func is not null)
            switch (level)
            {
                case LogLevel.Debug:
                case LogLevel.Trace:
                    _logger.Debug(exception, func(), parameters);
                    break;
                case LogLevel.Info:
                    _logger.Information(exception, func(), parameters);
                    break;
                case LogLevel.Warn:
                    _logger.Warning(exception, func(), parameters);
                    break;
                case LogLevel.Error:
                    _logger.Error(exception, func(), parameters);
                    break;
                case LogLevel.Fatal:
                    _logger.Fatal(exception, func(), parameters);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }

        return true;
    };

    public IDisposable OpenNestedContext(string message) => throw new NotImplementedException();

    public IDisposable OpenMappedContext(string key, object value, bool destructure = false) =>
        throw new NotImplementedException();
}