namespace ProductInventoryProject.Menu;

public interface ICommand
{
    string Description { get; }

    void Execute();
}
