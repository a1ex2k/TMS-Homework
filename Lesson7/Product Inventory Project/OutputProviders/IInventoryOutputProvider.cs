namespace ProductInventoryProject.OutputProviders;

public interface IInventoryOutputProvider
{
    void WriteInventory(Inventory inventory);
}
