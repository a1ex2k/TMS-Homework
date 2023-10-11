using System.Diagnostics;

namespace Loops;

internal static class ConsoleHelper
{

    public static int ReadInt32(string? prompt = null)
    {
        string? input;
        int value = 0;

        while (true)
        {
            PrintPrompt(prompt);
            input = Console.ReadLine();

            if (int.TryParse(input, out value))
            {
                break;
            }
            Console.WriteLine("Not an integer, try again.");
        }

        return value;
    }


    public static int ReadInt32(Predicate<int> predicate, string? prompt = null)
    {
        string? input;
        int value = 0;

        while (true)
        {
            PrintPrompt(prompt);
            input = Console.ReadLine();

            if (int.TryParse(input, out value) && predicate(value))
            {
                break;
            }
            Console.WriteLine("Incorrect input, or not in range.");
        }

        return value;
    }


    public static float ReadFloat(Predicate<float> predicate, string prompt)
    {
        string? input;
        float value = 0;

        while (true)
        {
            PrintPrompt(prompt);
            input = Console.ReadLine();

            if (float.TryParse(input, out value) && predicate(value))
            {
                break;
            }
            Console.WriteLine("Not a float in range, try again.");
        }

        return value;
    }

    public static void PrintPrompt(string? prompt = null)
    {
        Console.Write($"{prompt} > ");
    }


    public static void PrintArray<T>(T[] array)
    {
        Console.WriteLine($" [ {string.Join(", ", array)} ]");
    }


    public static void PrintArraySimple<T>(T[] array)
    {
        Console.WriteLine(string.Join(' ', array));
    }

}
