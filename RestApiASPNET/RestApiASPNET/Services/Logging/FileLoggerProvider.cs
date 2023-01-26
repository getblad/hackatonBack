namespace RestApiASPNET.Services.Logging;

public class FileLoggerProvider:ILoggerProvider
{
    string _path;

    public FileLoggerProvider(string path)
    {
        _path = path;
    }
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new FileLogger(_path);
    }
}

public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath)
    {
        builder.AddProvider(new FileLoggerProvider(filePath));
        return builder;
    }
}