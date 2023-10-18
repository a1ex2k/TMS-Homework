
using WorkingWithStrings;
using WorkingWithStrings.Abstract;
using WorkingWithStrings.Configuration;
using WorkingWithStrings.IoProviders;

var configuration = new AppConfiguration();

IInputProvider inputProvider = configuration.ReadFromFile
    ? new FileInputProvider(configuration.InputFilePath)
    : new ConsoleInputProvider("Введите Ваш текст", false);

IOutputProvider outputProvider = configuration.WriteToFile
    ? new FileOutputProvider(configuration.InputFilePath)
    : new ConsoleOutputProvider();

var menu = new StringOperationsMenu(inputProvider, outputProvider);
var menuCommandIndex = configuration.UseConfigMenuItem ? configuration.MenuItem ?? -1 : -1;
menu.Start(menuCommandIndex);
