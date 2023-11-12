using System.Text.Json.Serialization;

namespace ToDoListWeb.Data;

public class ToDoTask
{
    private readonly int _id;
    private readonly string _text;
    private readonly DateTime _createdAt;
    private DateTime? _completedAt;

    #region Properties

    /// <summary>
    /// Id of task
    /// </summary>
    public int Id => _id;

    /// <summary>
    /// Task text description
    /// </summary>
    public string Text => _text;

    /// <summary>
    /// Date and time of creation, <see cref="DateTime.Now"/> by default
    /// </summary>
    public DateTime CreatedAt => _createdAt;

    /// <summary>
    /// Date and time when marked as completed, <see cref="null"/> by default
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public DateTime? CompletedAt
    {
        get => _completedAt;
        set => _completedAt = value;
    }

    /// <summary>
    /// Task completion state, <see cref="false"/> when <see cref="ToDoTask.CompletedAt"/> is not set
    /// </summary>
    public bool IsCompleted => _completedAt is not null;

    #endregion

    #region Constructors

    /// <summary>
    /// Creates a new incomplete task with default ID, provided text, and current time
    /// </summary>
    /// <param name="text">Task text description</param>
    public ToDoTask(string text)
        : this(0, text, DateTime.Now, null)
    {
    }


    /// <summary>
    /// Creates a new incomplete task with provided text and current time
    /// </summary>
    /// <param name="id">Unique ID of task</param>
    /// <param name="text">Task text description</param>
    public ToDoTask(int id, string text)
        : this(id, text, DateTime.Now, null)
    {
    }


    /// <summary>
    /// Creates a new task with default ID
    /// </summary>
    /// <param name="text">Task text description</param>
    /// <param name="createdAt">Date and time of creation</param>
    /// <param name="completedAt">Date and time of completion</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public ToDoTask(string text, DateTime createdAt, DateTime? completedAt)
        : this(0, text, createdAt, completedAt)
    {
    }


    /// <summary>
    /// Creates a new task
    /// </summary>
    /// <param name="id">Unique ID of task</param>
    /// <param name="text">Task text description</param>
    /// <param name="createdAt">Date and time of creation</param>
    /// <param name="completedAt">Date and time of completion</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    [JsonConstructor]
    public ToDoTask(int id, string text, DateTime createdAt, DateTime? completedAt)
    {
        ArgumentNullException.ThrowIfNull(text);

        _id = id;
        _text = text;
        _createdAt = createdAt;
        _completedAt = completedAt;
    }

    #endregion


}