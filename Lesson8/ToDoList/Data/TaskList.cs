using System.Collections;

namespace ToDoList;

public class TaskList : IEnumerable<KeyValuePair<int, Task>>
{
    private Dictionary<int, Task> _tasks = new ();
    private int _nextId = 1;

    public int Count => _tasks.Count;
    public IEnumerable<Task> All => _tasks.Values;
    public IEnumerable<Task> Completed => _tasks.Values.Where(t => t.IsCompleted);
    public IEnumerable<Task> InCompleted => _tasks.Values.Where(t => !t.IsCompleted);


    public TaskList()
    {
    }


    public TaskList(IEnumerable<Task> tasks) : this()
    {
        Add(tasks);
    }


    public Task? GetById(int taskId)
    {
        return _tasks.TryGetValue(taskId, out var task) ? task : null;
    }


    public int Add(Task task)
    {
        var id = _nextId;
        _tasks[id] = task;
        _nextId++;
        return id;
    }


    public void Add(IEnumerable<Task> products)
    {
        foreach (var product in products)
        {
            Add(product);
        }
    }


    public bool TryRemove(int taskId)
    {
        return _tasks.Remove(taskId);
    }


    public IEnumerator<KeyValuePair<int, Task>> GetEnumerator()
    {
        return _tasks.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}