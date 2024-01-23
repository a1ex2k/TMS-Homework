using System.Buffers;
using LoginExceptions.Exceptions;

namespace LoginExceptions;

public static class Auth
{
    private const int MaxPasswordLength = 20;
    private const int MinPasswordLength = 6;
    private const int MaxLoginLength = 20;
    private const int MinLoginLength = 3;
 
    private static readonly SearchValues<char> DigitCharacters = SearchValues.Create("0123456789");
    private static readonly SearchValues<char> AcceptableCharacters =
        SearchValues.Create("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~`!@#$%^&*()_-+={[}]|\\:;\"'<,>.?/");

    public static bool LogIn(string username, string password, string passwordConfirm)
    {
        ThrowIfInvalidUsername(username);
        if (!string.Equals(password, passwordConfirm))
        {
            throw new WrongPasswordException("Passwords don't match", password);
        }

        ThrowIfInvalidPassword(password);
        return true;
    }

    private static void ThrowIfInvalidUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            throw new WrongLoginException("Username was empty", username);
        }
        
        if (username.Length < MinLoginLength)
        {
            throw new WrongLoginException($"Username length was less than {MinLoginLength}", username);
        }

        if (username.Length > MaxLoginLength)
        {
            throw new WrongLoginException($"Username length was greater than {MaxLoginLength}", username);
        }

        if (username.AsSpan().IndexOfAnyExcept(AcceptableCharacters) != -1)
        {
            throw new WrongLoginException("Username contains invalid characters", username);
        }
    }

    private static void ThrowIfInvalidPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new WrongPasswordException("Password was empty", password);
        }

        if (password.Length < MinLoginLength)
        {
            throw new WrongPasswordException($"Password length was less than {MinPasswordLength}", password);
        }

        if (password.Length > MaxLoginLength)
        {
            throw new WrongPasswordException($"Password length was greater than {MaxPasswordLength}", password);
        }

        if (password.AsSpan().IndexOfAny(DigitCharacters) == -1)
        {
            throw new WrongPasswordException("Password doesn't contain a digit", password);
        }

        if (password.AsSpan().IndexOfAnyExcept(AcceptableCharacters) != -1)
        {
            throw new WrongPasswordException("Password contains invalid characters", password);
        }
    }
}