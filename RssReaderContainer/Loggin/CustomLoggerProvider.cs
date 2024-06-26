using System.Collections.Concurrent;

namespace RssReaderContainer.Loggin;

public class CustomLoggerProvider : ILoggerProvider
{
    readonly CustomLoggerProviderConfiguration loggerConfig;
    readonly ConcurrentDictionary<string, CustomLogger> loggers = 
    new ConcurrentDictionary<string, CustomLogger>();
    public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
    {
        loggerConfig = config;
    }

    public ILogger CreateLogger(string category)
    {
        return loggers.GetOrAdd(category, name => new CustomLogger(name, loggerConfig));
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}