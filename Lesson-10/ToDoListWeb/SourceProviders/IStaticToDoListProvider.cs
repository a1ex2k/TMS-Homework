using System.Text.Json;
using ToDoListWeb.Data;

namespace ToDoListWeb.SourceProviders;

public interface IStaticToDoListProvider
{
    Task<List<ToDoTask>> LoadAsync();

    Task SaveAsync(List<ToDoTask> toDoList);
}


