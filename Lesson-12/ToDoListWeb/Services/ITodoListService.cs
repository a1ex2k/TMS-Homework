using ToDoListWeb.Data;

namespace ToDoListWeb.Services;

public interface ITodoListService
{
    Task<IEnumerable<ToDoTask>> GetAllAsync();

    Task<IEnumerable<ToDoTask>> GetCompletedAsync();

    Task<IEnumerable<ToDoTask>> GetNewAsync();

    Task<ToDoTask?> GetByIdAsync(int id);

    Task<ToDoTask> AddNewAsync(ToDoTask toDoTask);

    Task<ToDoTask?> RemoveByIdAsync(int id);

    Task<ToDoTask?> MarkAsCompletedAsync(int id, DateTime completedAt);

    Task<ToDoTask?> UpdateTextAsync(int id, string text);
}