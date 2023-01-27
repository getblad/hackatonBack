namespace RestApiASPNET.Services.Logging;

public class FileLogger:ILogger, IDisposable
{
    readonly string _filepath;
    static object _lock = new object();

    public FileLogger(string path)
    {
        _filepath = path;
    }
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    { 
        lock (_lock)
        {
            File.AppendAllText(_filepath, $"[{logLevel}]" + $" ({DateTime.UtcNow}) "+ formatter( state, exception) + Environment.NewLine);
        }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable BeginScope<TState>(TState state) 
    {
        return this;
    }

    public void Dispose() { }
    
    
}