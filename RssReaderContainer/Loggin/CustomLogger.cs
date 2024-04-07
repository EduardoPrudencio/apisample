namespace RssReaderContainer.Loggin;

public class CustomLogger: ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomLogger(string name, CustomLoggerProviderConfiguration config)
    {
        this.loggerName = name;
        this.loggerConfig = config;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string message = $"{logLevel.ToString()}:  {eventId.Id} - {formatter(state, exception)}";
        WriteMessage(message);
    }

    public void WriteMessage(string message)
    {
        string fileName = "Log.txt";

        using StreamWriter sw = new StreamWriter(fileName, true);
        sw.WriteLine(message);
    }
}