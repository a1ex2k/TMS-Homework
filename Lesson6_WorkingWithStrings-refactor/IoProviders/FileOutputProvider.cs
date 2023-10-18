using WorkingWithStrings.Abstract;

namespace WorkingWithStrings.IoProviders;

internal class FileOutputProvider : IOutputProvider
{
    private readonly string _outputFileName;

    public FileOutputProvider(string outputFileName)
    {
        _outputFileName = outputFileName;
    }

    public void WriteResult(string result)
    {
        File.WriteAllText(_outputFileName, result);
    }
}
