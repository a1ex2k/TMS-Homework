using System;
using static System.Net.Mime.MediaTypeNames;

namespace Loops;

internal static partial class Tasks
{
    public static void FibonacciWithLoop()
    {
        var first = 0;
        var second = 1;

        var count = 11;

        Console.Write($"{first} {second} ");

        for (int i = 2; i < count; i++)
        {
            var next = first + second;
            Console.Write($"{next} ");
            first = second;
            second = next;
        }
    }


    // FibonacciRecursive(0, 1, 1, number);
    public static void FibonacciRecursive(int first, int second, int counter, int number)
    {
        Console.Write($"{first} ");
        if (counter < number)
        {
            FibonacciRecursive(second, first + second, counter + 1, number);
        }
    }


    public static void Deposit()
    {
        var deposit = ConsoleHelper.ReadFloat(f => f > 0, "Deposit sum (positive)");
        var monthCount = ConsoleHelper.ReadInt32(i => i > 0, "Months count");
        var sum = deposit;

        for (int i = 0; i < monthCount; i++)
        {
            sum += sum * 1.07f;
        }

        Console.WriteLine($"{sum} after {monthCount} months");
    }


    public static void ArraysFiveToTen()
    {
        var arrayLength = ConsoleHelper.ReadInt32(v => v is > 5 and <= 10, "Array length (5, 10]");

        var firstArray = RandomArray(arrayLength, 0, 10);
        ConsoleHelper.PrintArray(firstArray);

        // it's from Microsoft's source of Array.FindAll(). Seriously??
        var list = new List<int>();
        for (int i = 0; i < firstArray.Length; i++)
        {
            if (firstArray[i] % 2 == 0)
            {
                list.Add(firstArray[i]);
            }
        }

        var secondArray = list.ToArray();
        ConsoleHelper.PrintArray(secondArray);
    }


    public static void ReplaceEvenIndexed()
    {
        var array = RandomArray(14, -20, 20);
        ConsoleHelper.PrintArray(array);

        for (int i = 0; i < array.Length; i++)
        {
            if (i % 2 == 1)
            {
                array[i] = 0;
            }
        }

        ConsoleHelper.PrintArray(array);
    }


    public static void StringArraySorting()
    {
        var names = new string[] { "Mathew", "Stephen", "Alex", "Juan", "Boris", "Peter", "Donald" };

        ConsoleHelper.PrintArray(names);
        BubbleSort(names);
        ConsoleHelper.PrintArray(names);
    }

    public static void BubbleSort<T>(T[] array) where T : IComparable<T>
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                if (array[j].CompareTo(array[j + 1]) > 0)
                {
                    var temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

}

