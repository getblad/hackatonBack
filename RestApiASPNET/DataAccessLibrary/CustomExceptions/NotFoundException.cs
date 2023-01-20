namespace DataAccessLibrary.CustomExceptions;

public class NotFoundException:Exception
{
    public string? MyMessage;

    public NotFoundException()
    {
    }

    public NotFoundException(int objectId)
    {
    }
    public NotFoundException(string? message) : base(message)
    {
        MyMessage = message;
    }

    public NotFoundException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }

    public NotFoundException(string? message, string objectName):this(message)
    {
        // this.objectName = objectName;
    }
}
