using System.Configuration;

namespace WorkingWithStrings.Abstract;

internal interface IConfiguration
{
    bool UseConfigMenuItem { get; }
    bool ReadFromFile { get; }
    bool WriteToFile { get; }
    int? MenuItem { get; }
    string InputFilePath { get; }
    string OutputFilePath { get; }

}