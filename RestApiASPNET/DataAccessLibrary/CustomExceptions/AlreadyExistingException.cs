namespace DataAccessLibrary.CustomExceptions;

public class AlreadyExistingException:Exception
{
    public AlreadyExistingException()
    {
    }

    public AlreadyExistingException(string? message) : base(message)
    {
    }
}