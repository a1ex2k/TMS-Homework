using System.Text;

namespace ToDoList.ConsoleManager;

public static class Extensions
{
    public static string ToMultilineString(this Task task)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Created at: {task.CreatedAt:dd.MM.yyyy, HH.mm.ss}");
        sb.AppendLine($"Text: {task.Text}");
        
        if (task.IsCompleted)
        {
            sb.AppendLine($"Completed ({task.CompletedAt:dd.MM.yyyy,HH.mm.ss})");
        }
        return sb.ToString();
    }

    public static string ToMenuString(this KeyValuePair<int, Task> taskPair)
    {
        return $"ID={taskPair.Key}  " + taskPair.Value.ToMultilineString();
    }
}