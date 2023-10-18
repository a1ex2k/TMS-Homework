namespace WorkingWithStrings.Abstract;

internal interface ICommand
{
    string Description { get; }

    void Execute();
}
