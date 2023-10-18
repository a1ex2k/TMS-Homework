using WorkingWithStrings.Abstract;

namespace WorkingWithStrings.Commands;

internal class MaxDigitWordsCommand : ResultCommandBase
{
    private readonly StringAnalyzer _stringAnalyzer;

    public MaxDigitWordsCommand(StringAnalyzer stringAnalyzer, IOutputProvider outputProvider)
        : base(outputProvider)
    {
        _stringAnalyzer = stringAnalyzer;
    }

    public override string Description => "Найти слова, содержащие максимальное количество цифр.";

    public override string GetResult()
    {
        return string.Join(", ",_stringAnalyzer.FindWordsWithMaxDigits());
    }
}
