using WorkingWithStrings.Abstract;

namespace WorkingWithStrings.Commands;

internal class LongestWordCommand : ResultCommandBase
{
    private readonly StringAnalyzer _stringAnalyzer;

    public LongestWordCommand(StringAnalyzer stringAnalyzer, IOutputProvider outputProvider)
        : base(outputProvider)
    {
        _stringAnalyzer = stringAnalyzer;
    }

    public override string Description => "Найти самое длинное слово и определить, сколько раз оно встретилось в тексте.";

    public override string GetResult()
    {
        var temp = _stringAnalyzer.FindLongestWord();

        return $"Самое длинное слово: {temp.Word} \nКоличество вхождений: {temp.Count}";
    }
}
