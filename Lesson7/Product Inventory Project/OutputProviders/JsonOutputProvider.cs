using System.Text.Json;

namespace ProductInventoryProject.OutputProviders;

public class JsonOutputProvider : IInventoryOutputProvider
{
    private readonly string _filePath;

    public JsonOutputProvider(string filePath)
    {
        _filePath = filePath;
    }

    public void WriteInventory(Inventory inventory)
    {
        var jsonString = JsonSerializer.Serialize(inventory.AsEnumerable());
        File.WriteAllText(_filePath, jsonString);
    }
}
