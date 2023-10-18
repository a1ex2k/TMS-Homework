using System.Text;
using WorkingWithStrings.Abstract;

namespace WorkingWithStrings.Commands;

internal class ReplaceDigitsCommand : ResultCommandBase
{
    private readonly StringAnalyzer _stringAnalyzer;

    public ReplaceDigitsCommand(StringAnalyzer stringAnalyzer, IOutputProvider outputProvider)
        : base(outputProvider)
    {
        _stringAnalyzer = stringAnalyzer;
    }

    public override string Description => "Заменить цифры словами.";

    public override string GetResult()
    {
        return _stringAnalyzer.ReplaceDigitsWithWords();
    }
}
