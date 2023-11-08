namespace ToDoListWeb.Models;

public class QueuedToDoTaskModel
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
}