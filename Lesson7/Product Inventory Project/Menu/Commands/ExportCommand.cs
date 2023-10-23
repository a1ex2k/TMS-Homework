using ProductInventoryProject.OutputProviders;

namespace ProductInventoryProject.Menu;

public class ExportCommand : ICommand
{
    private readonly Inventory _inventory;
    private readonly IInventoryOutputProvider _outputProvider;

    public ExportCommand(Inventory inventory, IInventoryOutputProvider outputProvider)
    {
        _inventory = inventory;
        _outputProvider = outputProvider;
    }

    public string Description { get; } = "Export To File";

    public void Execute()
    {
        _outputProvider.WriteInventory(_inventory);
        Console.WriteLine($"Export done!");
    }
}
