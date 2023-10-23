namespace ProductInventoryProject.SourceProviders;

public interface IInventorySourceProvider
{
    Inventory Load();

    void Save(Inventory inventory);
}