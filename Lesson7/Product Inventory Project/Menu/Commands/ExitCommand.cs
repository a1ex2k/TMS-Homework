namespace ProductInventoryProject.Menu;

public class ExitCommand : ICommand
{
    public string Description => "Quit";

    public void Execute()
    {
        Environment.Exit(0);
    }
}
