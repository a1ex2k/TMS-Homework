using System.Runtime.InteropServices;

namespace ConsoleMenu;

public class Menu
{
    private const string ExitKey = "q";

    private readonly string _title;
    private Dictionary<char, MenuItem> _items = new();

    public Menu(string title)
    {
        _title = title;
    }

    public void SetItem(char itemKey, string itemText, Action action)
    {
        _items[itemKey] = new MenuItem(itemText, action);
    }

    public void PrintMenu()
    {
        Write(_title, ConsoleColor.Yellow);
        foreach (var pair in _items)
        {
            Console.WriteLine($"    {pair.Key}  =>  {pair.Value}");
        }
        Console.WriteLine($"    q  =>  Exit this menu");

    }

    public bool HandleInput()
    {
        Write($"\r\nEnter menu item key > ", ConsoleColor.Yellow, false);

        while (true)
        {
            var input = Console.ReadLine();
            if (input == ExitKey)
            {
                return false;
            }
            
            if (input?.Length == 1 && _items.TryGetValue(input[0], out var menuItem))
            {
                Write($"Good. Selected item: {menuItem.Text}\r\n", ConsoleColor.Cyan);
                menuItem.Action.Invoke();
                Console.ReadLine();
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Write("Unrecognized key. Try again > ", ConsoleColor.Red, false);
        }
    }


    private void Write(string text, ConsoleColor color, bool newLine = true)
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        if (newLine)
            Console.WriteLine();
        Console.ForegroundColor = prevColor;
    }



    public void Run([Optional] Action? beforeMenuAction, bool runOnce = false)
    {
        do
        {
            Console.Clear();
            beforeMenuAction?.Invoke();

            PrintMenu();
            if (!HandleInput())
            {
                return;
            };
        }
        while (!runOnce);
    }

}