using WorkingWithStrings.Abstract;

namespace WorkingWithStrings.Commands;

internal class SentencesWithoutComasCommand : ResultCommandBase
{
    private readonly StringAnalyzer _stringAnalyzer;

    public SentencesWithoutComasCommand(StringAnalyzer stringAnalyzer, IOutputProvider outputProvider)
        : base(outputProvider)
    {
        _stringAnalyzer = stringAnalyzer;
    }

    public override string Description => "Найти предложения без запятых.";

    public override string GetResult()
    {
        return string.Join("\r\n",_stringAnalyzer.GetSentencesWithout(","));
    }
}
