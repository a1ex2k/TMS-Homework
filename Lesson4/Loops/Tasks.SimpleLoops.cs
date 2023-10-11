namespace Loops;

internal static partial class Tasks
{

    public static void OddNumbersOneToNinetyNine()
    {
        for (int number = 1; number <= 99; number++)
        {
            if (number % 2 != 0)
            {
                Console.Write($"{number} ");
            }
        }

        Console.WriteLine();
    }

    public static void NumbersFiveToOne()
    {
        for (int number = 5; number >= 1; number--)
        {
            Console.Write($"{number} ");
        }

        Console.WriteLine();
    }

    public static void SumZeroToN()
    {
        var maxNumber = ConsoleHelper.ReadInt32(v => v > 3, "Enter positive integer greater than 3");
        int sum = 1;
        for (int number = 2; number <= maxNumber; number++)
        {
            sum += number;
        }
        Console.Write($"Sum = {sum}");
    }

    public static void MagicSequence()
    {
        var number = 0;
        while (number < 98)
        {
            number += 7;
            Console.Write($"{number} ");
        }

        Console.WriteLine();
    }

    public static void TenOfSequence()
    {
        for (int count = 0, number = 0; count < 10; count++, number -= 5)
        {
            Console.Write($"{number} ");
        }

        Console.WriteLine();
    }

    public static void SquaresOfTenToTwenty()
    {
        for (int number = 10; number <= 20; number++)
        {
            Console.Write($"{number * number} ");
        }
        Console.WriteLine();
    }
}
