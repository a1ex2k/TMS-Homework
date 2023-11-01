using System.Text.Json;
using ToDoListWeb.Data;
using ToDoListWeb.Data;

namespace ToDoListWeb.SourceProviders;

public class JsonTodoListSourceProvider : IStaticToDoListProvider
{
    private readonly string _filePath;

    public JsonTodoListSourceProvider(string filePath)
    {
        _filePath = filePath;
    }

    public async Task<List<ToDoTask>> LoadAsync()
    {
        var json = await File.ReadAllTextAsync(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<ToDoTask>();
        }

        var taskList = JsonSerializer.Deserialize<List<ToDoTask>>(json);
        return taskList;
    }

    public async Task SaveAsync(List<ToDoTask> tasks)
    {
        var jsonString = JsonSerializer.Serialize(tasks);
        await File.WriteAllTextAsync(_filePath, jsonString);
    }
}