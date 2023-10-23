namespace ProductInventoryProject.OutputProviders;

public class TextFileOutputProvider : IInventoryOutputProvider
{
    private readonly string _filePath;

    public TextFileOutputProvider(string filePath)
    {
        _filePath = filePath;
    }

    public void WriteInventory(Inventory inventory)
    {
        File.WriteAllLines(_filePath, inventory.Select(p => p.ToString()));
    }
}
