namespace ProductInventoryProject.Menu;

public class PrintCommand : ICommand
{
    private readonly Inventory _inventory;

    public PrintCommand(Inventory inventory)
    {
        _inventory = inventory;
    }

    public string Description { get; } = "Print";

    public void Execute()
    {
        if (_inventory.Count == 0)
        {
            Console.WriteLine($" There is no products in Inventory");
            return;
        }

        var counter = 0;
        foreach (var product in _inventory)
        {
            Console.WriteLine($"  [{counter++}]  {product}");
        }
    }
}
