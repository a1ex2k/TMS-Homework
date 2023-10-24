namespace ToDoList.SourceProviders;

public interface ITodoListSourceProvider
{
    TaskList Load();

    void Save(TaskList taskList);
}