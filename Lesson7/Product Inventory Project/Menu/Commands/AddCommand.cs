using System.Globalization;
using ProductInventoryProject.OutputProviders;

namespace ProductInventoryProject.Menu;

public class AddCommand : ICommand
{
    private readonly Inventory _inventory;

    public AddCommand(Inventory inventory)
    {
        _inventory = inventory;
    }

    public string Description { get; } = "Add Product";

    public void Execute()
    {
        Console.Write("Enter description > ");
        var description = Console.ReadLine();
        Console.Write("Enter price > ");
        var priceString = Console.ReadLine();

        if (!decimal.TryParse(priceString, NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
        {
            Console.WriteLine("Invalid price, aborted.");
            return;
        }

        var product = new Product(price, description);
        var id = _inventory.Add(product);

        Console.WriteLine($"Added with ID = {id}:\r\n\t{product}");
    }
}
