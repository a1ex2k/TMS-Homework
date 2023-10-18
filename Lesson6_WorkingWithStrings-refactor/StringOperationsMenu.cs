using WorkingWithStrings.Abstract;
using WorkingWithStrings.Commands;

namespace WorkingWithStrings;

internal class StringOperationsMenu
{
    private readonly IInputProvider _inputProvider;
    private readonly IOutputProvider _outputProvider;

    public StringOperationsMenu(IInputProvider inputProvider, IOutputProvider outputProvider)
    {
        _inputProvider = inputProvider;
        _outputProvider = outputProvider;
    }

    public void Start(int commandNumber = -1)
    {
        var text = _inputProvider.Read();
        var stringAnalyzer = new StringAnalyzer(text);

        var commands = new List<ICommand>
        {
            new ExitCommand(),
            new MaxDigitWordsCommand(stringAnalyzer, _outputProvider),
            new LongestWordCommand(stringAnalyzer, _outputProvider),
            new ReplaceDigitsCommand(stringAnalyzer, _outputProvider),
            new SentencesWithoutComasCommand(stringAnalyzer, _outputProvider),
        };

        if (commandNumber >= 0 && commandNumber < commands.Count)
        {
            commands[commandNumber].Execute();
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите операцию\r\n");

            for (var i = 0; i < commands.Count; i++)
            {
                Console.WriteLine($"{i} => {commands[i].Description}");
            }

            var isParsed = int.TryParse(Console.ReadLine(), out commandNumber);

            if (isParsed && commandNumber < commands.Count)
            {
                commands[commandNumber].Execute();
            }
            else
            {
                Console.WriteLine($"Доступны только циферки от 0 до {commands.Count - 1}");
                Console.ReadLine();
            }

        }
    }

}
