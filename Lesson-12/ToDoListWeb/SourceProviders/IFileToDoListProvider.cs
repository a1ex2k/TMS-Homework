using ToDoListWeb.Data;

namespace ToDoListWeb.SourceProviders;

public interface IFileToDoListProvider
{
    Task<List<ToDoTask>> LoadAsync();

    Task SaveAsync(List<ToDoTask> toDoList);
}


