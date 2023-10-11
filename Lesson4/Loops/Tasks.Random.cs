namespace Loops;

internal static partial class Tasks
{
    public static readonly Random Random = new Random();

    public static int[] RandomArray(int length, int minValue = 0, int maxValue = 1000)
    {
        var array = new int[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = Random.Next(minValue, maxValue);
        }
        return array;
    }

}
