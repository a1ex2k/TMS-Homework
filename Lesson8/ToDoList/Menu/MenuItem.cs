using System.Net.Mime;

namespace ConsoleMenu;

public class MenuItem
{
    public readonly string Text;

    public readonly Action Action;

    public MenuItem(string text, Action action)
    {
        Text = text;
        Action = action;
    }


    public override string ToString() => Text;
}