using System.Configuration;
using WorkingWithStrings.Abstract;

namespace WorkingWithStrings.Configuration;

internal class AppConfiguration : IConfiguration
{
    public bool UseConfigMenuItem { get; private set; }
    public bool ReadFromFile { get; private set; }
    public bool WriteToFile { get; private set; }
    public int? MenuItem { get; private set; }
    public string InputFilePath { get; private set; }
    public string OutputFilePath { get; private set; }

    public AppConfiguration()
    {
        ReadValues();
    }

    public void ReadValues()
    {
        ConfigurationManager.RefreshSection("appSettings");

        InputFilePath = ConfigurationManager.AppSettings[nameof(InputFilePath)] ?? "defaultInput.txt";
        OutputFilePath = ConfigurationManager.AppSettings[nameof(OutputFilePath)] ?? "defaultOutput.txt";
        MenuItem = GetInt32Value(nameof(MenuItem));

        UseConfigMenuItem = GetBooleanValue(nameof(UseConfigMenuItem));
        ReadFromFile = GetBooleanValue(nameof(ReadFromFile));
        WriteToFile = GetBooleanValue(nameof(WriteToFile));
    }

    private bool GetBooleanValue(string key)
    {
        var stringValue = ConfigurationManager.AppSettings[key];
        bool.TryParse(stringValue, out var booleanValue);
        return booleanValue;
    }

    private int? GetInt32Value(string key)
    {
        var stringValue = ConfigurationManager.AppSettings[key];
        return int.TryParse(stringValue, out var intValue) ? intValue : null;
    }

}