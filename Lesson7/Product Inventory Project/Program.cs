using Microsoft.Extensions.Configuration;
using ProductInventoryProject.Menu;
using ProductInventoryProject.OutputProviders;
using ProductInventoryProject.SourceProviders;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
var inventorySection = config.GetRequiredSection("InventoryIOOptions");


IInventorySourceProvider sourceProvider = inventorySection["InventorySourceType"] switch
{
    "json" => new JsonInventorySourceProvider(inventorySection["InventorySource"]),
    _ => throw new ArgumentException()
};

IInventoryOutputProvider outputProvider = inventorySection["OutputTargetType"] switch
{
    "console" => new ConsoleOutputProvider(),
    "json" => new JsonOutputProvider(inventorySection["JsonOutputFilePath"]),
    "text" => new TextFileOutputProvider(inventorySection["TextOutputFilePath"]),
    _ => throw new ArgumentException()
};


var menu = new InventoryConsoleMenu(sourceProvider, outputProvider);
menu.LoadInventory();
menu.Start();