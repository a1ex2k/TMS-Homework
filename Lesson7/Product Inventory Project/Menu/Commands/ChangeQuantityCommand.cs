namespace ProductInventoryProject.Menu;

public class ChangeQuantityCommand : ICommand
{
    private readonly Inventory _inventory;

    public ChangeQuantityCommand(Inventory inventory)
    {
        _inventory = inventory;
    }

    public string Description { get; } = "Change Quantity";

    public void Execute()
    {
        Console.Write("Enter product ID > ");
        var idString = Console.ReadLine();

        if (!int.TryParse(idString, out var id))
        {
            Console.WriteLine("Invalid ID, aborted.");
            return;
        }

        var product = _inventory.GetById(id);

        if (product is null)
        {
            Console.WriteLine("No product with such ID, aborted.");
            return;
        }
        Console.WriteLine(product);
        Console.Write("Enter count to add (positive) or remove (negative) > ");
        var countString = Console.ReadLine();

        if (!int.TryParse(countString, out var count))
        {
            Console.WriteLine("Invalid number, aborted.");
            return;
        }

        if (count < 0 && (-count) > product.Quantity)
        {
            Console.WriteLine("There is no enough quantity, aborted.");
            return;
        }

        product.ChangeQuantity(count);
        Console.WriteLine($"Changed successfully. Quantity: {product.Quantity}");
    }
}
