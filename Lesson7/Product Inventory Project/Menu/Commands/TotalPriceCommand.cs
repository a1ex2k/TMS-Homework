namespace ProductInventoryProject.Menu;

public class TotalPriceCommand : ICommand
{
    private readonly Inventory _inventory;

    public TotalPriceCommand(Inventory inventory)
    {
        _inventory = inventory;
    }

    public string Description => "Total Price";

    public void Execute()
    {
        Console.WriteLine($"Inventory has {_inventory.Count} positions. Total price: {_inventory.TotalPrice}");
    }
}
