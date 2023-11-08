using ToDoListWeb.Data;
using ToDoListWeb.Models;

namespace ToDoListWeb.Utility;

public static class Extensions
{
    public static ToDoTask Copy(this ToDoTask toDoTask)
    {
        return new ToDoTask(toDoTask.Id, toDoTask.Text, toDoTask.CreatedAt, toDoTask.CompletedAt);
    }


    public static ToDoTaskModel ToTaskModel(this ToDoTask toDoTask)
    {
        return new ToDoTaskModel
        {
            Id = toDoTask.Id,
            Text = toDoTask.Text,
            CreatedAt = toDoTask.CreatedAt,
            CompletedAt = toDoTask.CompletedAt
        };
    }
    

    public static QueuedToDoTaskModel ToQueuedTaskModel(this ToDoTask toDoTask)
    {
        return new QueuedToDoTaskModel()
        {
            Id = toDoTask.Id,
            Text = toDoTask.Text,
            CreatedAt = toDoTask.CreatedAt,
        };
    }

}