namespace ToDoListWeb.Models;

public class DetailsModel
{
    public ToDoTaskModel ToDoTask { get; set; }

    public List<QueuedToDoTaskModel> QueuedTasks { get; set; } = new();
}