using System.Text.Json;

namespace ProductInventoryProject.SourceProviders;

public class JsonInventorySourceProvider : IInventorySourceProvider
{
    private readonly string _filePath;

    public JsonInventorySourceProvider(string filePath)
    {
        _filePath = filePath;
    }

    public Inventory Load()
    {
        var json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json)) return new Inventory();

        var products = JsonSerializer.Deserialize<IEnumerable<ProductData>>(json)?
                .Select(pd => new Product(pd.Price, pd.Quantity, pd.Description));

        var inventory = products is null ? new Inventory() : new Inventory(products);
        return inventory;
    }

    public void Save(Inventory inventory)
    {
        var jsonProducts = inventory.AsEnumerable()
            .Select(p => new ProductData
            {
                Quantity = p.Quantity,
                Price = p.Price,
                Description = p.Description
            });
        
        var jsonString = JsonSerializer.Serialize(jsonProducts);
        File.WriteAllText(_filePath, jsonString);
    }
}