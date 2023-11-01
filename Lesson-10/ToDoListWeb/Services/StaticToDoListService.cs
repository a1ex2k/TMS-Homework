using System.Collections.Concurrent;
using ToDoListWeb.Data;
using ToDoListWeb.Utility;
using ToDoListWeb.SourceProviders;

namespace ToDoListWeb.Services;

public class StaticToDoListService : ITodoListService
{
    private readonly IStaticToDoListProvider _sourceProvider;
    private readonly ILogger<StaticToDoListService> _logger;
    private ConcurrentDictionary<int, ToDoTask> _tasks;
    private readonly SemaphoreSlim _addingSemaphore = new SemaphoreSlim(1);
    private readonly SemaphoreSlim _savingSemaphore = new SemaphoreSlim(1);
    private int _nextId = 1;

    public StaticToDoListService(IStaticToDoListProvider sourceProvider, ILogger<StaticToDoListService> logger)
    {
        _sourceProvider = sourceProvider;
        _logger = logger;

        //Этому место явно не в конструкторе. Куда лучше?
        LoadListAsync().Wait();
    }

    private async Task LoadListAsync()
    {
        var taskList = await _sourceProvider.LoadAsync();
        var taskDictionary = new ConcurrentDictionary<int, ToDoTask>();
        
        foreach (var task in taskList)
        {
            taskDictionary[task.Id] = task;
        }

        await _addingSemaphore.WaitAsync();
        _tasks = taskDictionary;
        _nextId = _tasks.Count == 0 ? 1 : (_tasks.Keys.Max() + 1);
        _logger.LogInformation("Loaded {Count} tasks from static source", taskDictionary.Count);
        _addingSemaphore.Release();
    }

    private async Task SaveListAsync()
    {
        await _savingSemaphore.WaitAsync();
        await _sourceProvider.SaveAsync(_tasks.Values.ToList());
        _savingSemaphore.Release();
    }

    public async Task<IEnumerable<ToDoTask>> GetAllAsync()
    {
        return _tasks.Values
            .Select(t => t.Copy());
    }

    public async Task<IEnumerable<ToDoTask>> GetCompletedAsync()
    {
        return _tasks.Values
            .Where(t => t.IsCompleted)
            .Select(t => t.Copy());
    }

    public async Task<IEnumerable<ToDoTask>> GetNewAsync()
    {
        return _tasks.Values
            .Where(t => !t.IsCompleted)
            .Select(t => t.Copy());
    }

    public async Task<ToDoTask?> GetByIdAsync(int id)
    {
        return _tasks.TryGetValue(id, out var task) ? task : null;
    }


    public async Task<ToDoTask> AddNewAsync(ToDoTask toDoTask)
    {
        await _addingSemaphore.WaitAsync();
        var newTask = new ToDoTask(_nextId, toDoTask.Text, toDoTask.CreatedAt, toDoTask.CompletedAt);
        _tasks[newTask.Id] = newTask;
        _nextId++;
        await SaveListAsync();
        _logger.LogInformation("Added new task with ID = {Id}", newTask.Id);
        _addingSemaphore.Release();
        return newTask.Copy();
    }

    public async Task<ToDoTask?> RemoveByIdAsync(int id)
    {
        _tasks.TryRemove(id, out var task);
        if (task is not null)
        {
            await SaveListAsync();
        }

        return task;
    }

    public async Task<ToDoTask?> MarkAsCompletedAsync(int id, DateTime dateTime)
    {
        if (!_tasks.TryGetValue(id, out var task))
        {
            return null;
        }
        
        task.MarkAsCompleted(dateTime);
        await SaveListAsync();
        return task.Copy();
    }
}
