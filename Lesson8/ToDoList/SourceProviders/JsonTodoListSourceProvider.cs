using System.Text.Json;

namespace ToDoList.SourceProviders;

public class JsonTodoListSourceProvider : ITodoListSourceProvider
{
    private readonly string _filePath;

    public JsonTodoListSourceProvider(string filePath)
    {
        _filePath = filePath;
    }

    public TaskList Load()
    {
        var json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new TaskList();
        }

        var tasks = JsonSerializer.Deserialize<IEnumerable<Task>>(json);
        var taskList = tasks is null ? new TaskList() : new TaskList(tasks);
        return taskList;
    }

    public void Save(TaskList taskList)
    {
        var jsonString = JsonSerializer.Serialize(taskList.All);
        File.WriteAllText(_filePath, jsonString);
    }
}