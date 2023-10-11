using System.Globalization;

namespace MatrixOperations;

public static class InputHelper
{
    private static Random Random = new Random();
    
    public static int ReadInt32(Predicate<int> predicate, string prompt)
    {
        int value = 0;
        
        while (true)
        {
            Console.Write($"{prompt} > ");
            var input = Console.ReadLine();
            if (int.TryParse(input, out value) && predicate(value))
            {
                break;
            }
            Console.WriteLine("Некорректный ввод.");
        }

        return value;
    }


    public static double ReadMatrixElementOrRandom(int rowIndex, int columnIndex)
    {
        Console.Write($"M[{rowIndex},{columnIndex}] = ");
        var line = Console.ReadLine();
        if (!double.TryParse(line, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
        {
            return Math.Round(Random.Next(-1999, 1000)*Random.NextDouble());
        }
        return value;
    }

}