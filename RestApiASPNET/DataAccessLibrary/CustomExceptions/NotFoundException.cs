namespace DataAccessLibrary.CustomExceptions;

public class NotFoundException:System.Exception
{
   public string ObjectName;

    public NotFoundException()
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }

    public NotFoundException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }

    public NotFoundException(string? message, string objectName):this(message)
    {
        ObjectName = objectName;
    }
}
