using ConsoleMenu;
using ToDoList;
using ToDoList.ConsoleManager;
using ToDoList.SourceProviders;
using Task = ToDoList.Task;

namespace ProductInventoryProject.ConsoleManager;

public class ToDoListConsoleManager
{
    private readonly ITodoListSourceProvider _sourceProvider;
    private TaskList _taskList;
    private Menu _menu;
    private readonly bool _autosave;

    public ToDoListConsoleManager(ITodoListSourceProvider sourceProvider, bool autosave)
    {
        _sourceProvider = sourceProvider;
        _autosave = autosave;
        _menu = CreateMenu();
    }

    private Menu CreateMenu()
    {
        var menu = new Menu($"[Autosave {(_autosave ? "on" : "off")}]  ToDoList options:");
        menu.SetItem("a", "Add new task", AddNewTask);
        menu.SetItem("m", "Mark task as completed", MarkAsCompleted);
        menu.SetItem("r", "Remove task", RemoveTask);
        menu.SetItem("s", "Save changes", SaveChanges);
        return menu;
    }

    public void LoadTasks()
    {
        try
        {
            _taskList = _sourceProvider.Load();
        }
        catch (Exception ex)
        {
            _menu.PrintError("Cannot read source");
            Console.WriteLine(ex);
            Console.ReadLine();
        }
    }

    public void Run()
    {
        if (_taskList is null)
        {
            _menu.PrintError("ToDoList wasn't loaded. Enter to quit...");
            Console.ReadLine();
            return;
        }

        _menu.Run(PrintAll);
    }


    private void PrintAll()
    {
        Console.WriteLine("- - - New - - -");
        foreach (var pair in _taskList.Where(p => !p.Value.IsCompleted))
        {
            Console.WriteLine(pair.ToMenuString());
        }

        Console.WriteLine("- - - Completed - - -");
        foreach (var pair in _taskList.Where(p => p.Value.IsCompleted))
        {
            Console.WriteLine(pair.ToMenuString());
        }
    }



    private void AddNewTask()
    {
        Console.Write("Enter task text > ");
        var text = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(text))
        {
            _menu.PrintError("Nothing typed, nothing added.");
            return;
        }

        var task = new ToDoList.Task(text);
        var id = _taskList.Add(task);

        Console.WriteLine($"Added with ID = {id} at {task.CreatedAt:HH:mm:ss}");

        if (_autosave)
        {
            SaveChanges();
        }
    }


    private void MarkAsCompleted()
    {
        var taskPair = RequestTask();
        if (taskPair is null)
        {
            return;
        }

        var task = taskPair.Value.Task;

        if (task.IsCompleted)
        {
            _menu.PrintError("Already completed");
            return;
        }

        task.MarkAsCompleted();
        Console.WriteLine($"Marked as completed at {task.CompletedAt:HH:mm:ss}");

        if (_autosave)
        {
            SaveChanges();
        }
    }


    private void RemoveTask()
    {
        var taskPair = RequestTask();
        if (taskPair is null)
        {
            return;
        }

        Console.WriteLine($"Enter 'remove' to continue > ");
        if (!string.Equals(Console.ReadLine(), "remove", StringComparison.OrdinalIgnoreCase))
        {
            _menu.PrintError("Removing aborted.");
        }

        _taskList.TryRemove(taskPair.Value.Id);
        Console.WriteLine($"Task removed");
        if (_autosave)
        {
            SaveChanges();
        }
    }


    private void SaveChanges()
    {
        try
        {
            _sourceProvider.Save(_taskList);
        }
        catch (Exception ex)
        {
            _menu.PrintError("Cannot read source");
            Console.WriteLine(ex);
            Console.ReadLine();
        }
        
        Console.WriteLine("Changes saved");
    }


    private (int Id, Task Task)? RequestTask()
    {
        Console.Write("Enter task ID > ");
        var idString = Console.ReadLine();

        if (!int.TryParse(idString, out var id))
        {
            _menu.PrintError("Invalid ID, aborted.");
            return null;
        }

        var task = _taskList.GetById(id);

        if (task is null)
        {
            _menu.PrintError("No product with such ID, aborted.");
            return null;
        }
        
        Console.WriteLine($"Selected:\r\n{task.ToMultilineString()}");
        return (id, task);
    }

}
