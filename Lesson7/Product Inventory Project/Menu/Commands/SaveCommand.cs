using ProductInventoryProject.OutputProviders;
using ProductInventoryProject.SourceProviders;

namespace ProductInventoryProject.Menu;

public class SaveCommand : ICommand
{
    private readonly Inventory _inventory;
    private readonly IInventorySourceProvider _sourceProvider;

    public SaveCommand(Inventory inventory, IInventorySourceProvider sourceProvider)
    {
        _inventory = inventory;
        _sourceProvider = sourceProvider;
    }

    public string Description { get; } = "Save changes";

    public void Execute()
    {
        try
        {
            _sourceProvider.Save(_inventory);
        }
        catch
        {
            Console.WriteLine($"Cannot save inventory.");
        }
        Console.WriteLine($"Saved!");
    }
}
