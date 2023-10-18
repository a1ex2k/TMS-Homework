using System.ComponentModel;
using System.Text;
using WorkingWithStrings.Abstract;

namespace WorkingWithStrings.IoProviders;

internal class ConsoleInputProvider : IInputProvider
{
    private readonly string? _prompt;
    private readonly bool _multiline;

    public ConsoleInputProvider(string? prompt, bool multiline)
    {
        _prompt = prompt;
        _multiline = multiline;
    }

    public string Read()
    {
        if (_multiline)
        {
            return ReadMultiline();
        }

        return ReadSingleLine();
    }
    
    private string ReadMultiline()
    {
        Console.WriteLine($"{_prompt} (1 or more lines, Control+Z to end input)");
        var sb = new StringBuilder();

        string? line;
        while ((line = Console.ReadLine()) != null)
        {
            sb.Append(line);
        }

        return sb.ToString();
    }

    private string ReadSingleLine()
    {
        Console.Write($"{_prompt} > ");
        var line = Console.ReadLine();

        return line ?? String.Empty
    }
}
