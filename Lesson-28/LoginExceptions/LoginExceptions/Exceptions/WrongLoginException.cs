namespace LoginExceptions.Exceptions;

public class WrongLoginException : Exception
{
    public string Username { get; }

    public WrongLoginException(string? message, string username) : base(message)
    {
        Username = username;
    }

    public WrongLoginException(string username) : base("Username doesn't match requirements")
    {
        Username = username;
    }
}