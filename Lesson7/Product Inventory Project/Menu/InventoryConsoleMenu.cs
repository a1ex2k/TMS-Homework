using ProductInventoryProject.OutputProviders;
using ProductInventoryProject.SourceProviders;

namespace ProductInventoryProject.Menu;

public class InventoryConsoleMenu
{
    private readonly IInventorySourceProvider _sourceProvider;
    private readonly IInventoryOutputProvider _outputProvider;
    private Inventory _inventory;
    private List<ICommand> _commands;

    public InventoryConsoleMenu(IInventorySourceProvider sourceProvider, IInventoryOutputProvider outputProvider)
    {
        _sourceProvider = sourceProvider;
        _outputProvider = outputProvider;
    }

    public void LoadInventory()
    {
        try
        {
            _inventory = _sourceProvider.Load();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Cannot read inventory");
            Console.WriteLine(ex);
            Console.ReadLine();
        }
    }

    public void Start()
    {
        if (_inventory is null)
        {
            Console.WriteLine("Inventory wasn't loaded");
            Console.ReadLine();
            return;
        }

        _commands = new List<ICommand>()
        {
            new ExitCommand(),
            new PrintCommand(_inventory),
            new AddCommand(_inventory),
            new ChangeQuantityCommand(_inventory),
            new TotalPriceCommand(_inventory),
            new ExportCommand(_inventory, _outputProvider),
            new SaveCommand(_inventory, _sourceProvider)
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select option\r\n");

            for (var i = 0; i < _commands.Count; i++)
            {
                Console.WriteLine($"{i} => {_commands[i].Description}");
            }

            Console.WriteLine();
            Console.Write("> ");

            var isParsed = int.TryParse(Console.ReadLine(), out var commandNumber);

            if (isParsed && commandNumber < _commands.Count)
            {
                _commands[commandNumber].Execute();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Incorrect index.");
                Console.ReadLine();
            }

        }
    }

}
