using Microsoft.Extensions.Configuration;
using ProductInventoryProject.ConsoleManager;
using ToDoList.SourceProviders;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var todoListSection = config.GetRequiredSection("ToDoList");


ITodoListSourceProvider sourceProvider = todoListSection["SourceType"] switch
{
    "json" => new JsonTodoListSourceProvider(todoListSection["JsonSource"]),
    _ => throw new ArgumentException()
};

var autoSave = bool.TryParse(todoListSection["Autosave"], out var value) && value;


var manager = new ToDoListConsoleManager(sourceProvider, autoSave);
manager.LoadTasks();
manager.Run();