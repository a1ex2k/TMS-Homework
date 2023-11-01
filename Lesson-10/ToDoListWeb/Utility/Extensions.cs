using ToDoListWeb.Data;

namespace ToDoListWeb.Utility;

public static class Extensions
{
    public static ToDoTask Copy(this ToDoTask toDoTask)
    {
        return new ToDoTask(toDoTask.Id, toDoTask.Text, toDoTask.CreatedAt, toDoTask.CompletedAt);
    }

    public static ToDoTaskDto ToDto(this ToDoTask toDoTask)
    {
        return new ToDoTaskDto
        {
            Id = toDoTask.Id,
            Text = toDoTask.Text,
            CreatedAt = toDoTask.CreatedAt,
            CompletedAt = toDoTask.CompletedAt
        };
    }

    public static ToDoListDto ToDto(this IEnumerable<ToDoTask> toDoTasks)
    {
        return new ToDoListDto()
        {
            ToDoTasks = toDoTasks.Select(t => t.ToDto()).ToList(),
        };
    }
}