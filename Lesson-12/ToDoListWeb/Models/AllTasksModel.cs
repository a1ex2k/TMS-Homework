namespace ToDoListWeb.Models;

public class AllTasksModel
{
    public List<QueuedToDoTaskModel> QueuedTasks { get; set; } = new();

    public List<ToDoTaskModel> CompletedTasks { get; set; } = new();
}