using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loops;

internal static partial class Tasks
{

    public static void ArrayContainsNumber()
    {
        var array = RandomArray(Random.Next(6, 26));
        ConsoleHelper.PrintArray(array);
        var number = ConsoleHelper.ReadInt32("Number to find");
        var exists = Array.IndexOf(array, number) >= 0;
        Console.WriteLine($"{number} exists in array: {exists}");
    }

    public static void RemoveNumber()
    {
        var array = RandomArray(20, 1, 5);
        ConsoleHelper.PrintArray(array);
        var number = ConsoleHelper.ReadInt32("Number to find");
        var exists = Array.IndexOf(array, number) >= 0;
        if (!exists)
        {
            Console.WriteLine($"{number} not exists in array");
            return;
        }
        var newArray = Array.FindAll(array, n => n != number);
        ConsoleHelper.PrintArray(newArray);
    }

    public static void ArrayMinMax()
    {
        var length = ConsoleHelper.ReadInt32(v => v >= 3, "Array length, N >= 3");
        var array = RandomArray(length, 1, 100);
        ConsoleHelper.PrintArray(array);

        var min = int.MaxValue;
        var max = int.MinValue;
        var sum = 0;

        for (int i = 0; i < length; i++)
        {
            sum += array[i];
            if (array[i] < min)
            {
                min = array[i];
            }
            if (array[i] > max)
            {
                max = array[i];
            }
        }

        Console.WriteLine($"min = {min}; max = {max}; avg = {(double)sum / length}");
    }

    public static void TwoArrays()
    {
        var length = 5;
        var firstArray = RandomArray(length, 1, 100);
        var secondArray = RandomArray(length, 1, 100);

        var firstArraySum = 0;
        var secondArraySum = 0;

        for (int i = 0; i < length; i++)
        {
            firstArraySum += firstArray[i];
            secondArraySum += secondArray[i];
        }

        var firstArrayAverage = (double)firstArraySum / length;
        var secondArrayAverage = (double)secondArraySum / length;
        Console.WriteLine($"avg(1) = {firstArrayAverage}; avg(2) = {secondArrayAverage}");

        if (firstArrayAverage == secondArrayAverage)
        {
            Console.WriteLine($"Arrays average values are equal");
        }
        else if (firstArrayAverage > secondArrayAverage)
        {
            Console.WriteLine($"First's average is greater");
        }
        else
        {
            Console.WriteLine($"Second's average is greater");
        }
    }
}
