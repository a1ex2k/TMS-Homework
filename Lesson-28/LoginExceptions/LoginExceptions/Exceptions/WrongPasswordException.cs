namespace LoginExceptions.Exceptions;

public class WrongPasswordException : Exception
{
    public string Password { get; }

    public WrongPasswordException(string? message, string password) : base(message)
    {
        Password = password;
    }

    public WrongPasswordException(string password) : base("Username doesn't match requirements")
    {
        Password = password;
    }
}