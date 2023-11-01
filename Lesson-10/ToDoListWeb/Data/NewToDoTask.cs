using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace ToDoListWeb.Data;

public class NewToDoTask
{
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}