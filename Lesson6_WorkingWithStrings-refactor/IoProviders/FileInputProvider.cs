using WorkingWithStrings.Abstract;

namespace WorkingWithStrings.IoProviders;

internal class FileInputProvider : IInputProvider
{
    private readonly string _inputFileName;

    public FileInputProvider(string inputFileName)
    {
        _inputFileName = inputFileName;
    }
    
    public string Read()
    {
        var text = File.ReadAllText(_inputFileName);
        return text;
    }
}
