using LoginExceptions;
using LoginExceptions.Exceptions;

HandleLoginExceptions("", "", "");
HandleLoginExceptions("us", "skdfh", "skdfhhnb");
HandleLoginExceptions("us", "skdfhggg", "skdfhggg");
HandleLoginExceptions("us555", "skdfhggg", "skdfhggg0");
HandleLoginExceptions("us555", "skdfhggg0", "skdfhggg");
HandleLoginExceptions("us555", "skdfhggg0", "skdfhggg0");
HandleLoginExceptions("us555", "skdfhggg", "skdfhggg");
HandleLoginExceptions("Яus555", "skdfhggg0", "skdfhggg0");
HandleLoginExceptions("us555", "skdfhggg0Я", "skdfhggg0Я");
void HandleLoginExceptions(string username, string password, string passwordConfirm)
{
    Console.WriteLine($"\"{username}\" | \"{password}\" | \"{passwordConfirm}\"");

    try
    {
        var success = Auth.LogIn(username, password, passwordConfirm);
        Console.WriteLine(success);
    }
    catch (WrongLoginException ex)
    {
        Console.WriteLine($"{nameof(WrongLoginException)}: {ex.Message}");
    }
    catch (WrongPasswordException ex)
    {
        Console.WriteLine($"{nameof(WrongPasswordException)}: {ex.Message}");
    }

    Console.WriteLine();
}