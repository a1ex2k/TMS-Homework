using System.Text;

namespace ToDoList.ConsoleManager;

public static class Extensions
{
    public static string ToMultilineString(this Task task)
    {
        var sb = new StringBuilder();
        sb.Append($"Created: {task.CreatedAt:dd.MM.yyyy, HH:mm:ss}   ");
        if (task.IsCompleted)
        {
            sb.Append($"Completed: {task.CompletedAt:dd.MM.yyyy, HH:mm:ss}");
        }

        sb.AppendLine();
        sb.AppendLine(task.Text);
        
        return sb.ToString();
    }

    public static string ToMenuString(this KeyValuePair<int, Task> taskPair)
    {
        return $"ID: {taskPair.Key}   " + taskPair.Value.ToMultilineString();
    }
}