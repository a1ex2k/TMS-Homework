using WorkingWithStrings.Abstract;

namespace WorkingWithStrings.IoProviders;

internal class ConsoleOutputProvider : IOutputProvider
{
    public void WriteResult(string result)
    {
        Console.WriteLine(result);
        Console.ReadLine();
    }
}
