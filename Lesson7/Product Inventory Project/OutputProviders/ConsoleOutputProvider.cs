namespace ProductInventoryProject.OutputProviders;

public class ConsoleOutputProvider : IInventoryOutputProvider
{
    public void WriteInventory(Inventory inventory)
    {
        foreach (var product in inventory)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine();
    }
}
