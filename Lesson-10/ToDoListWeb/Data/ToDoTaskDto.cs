using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace ToDoListWeb.Data;

public class ToDoTaskDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}