using System.Diagnostics;
using System.Reflection;

namespace ProverbsRandomizer.ConsoleApp.Dynamic;

internal class Program
{
    public static void Main(string[] args)
    {
        MethodInfo? loadMethod = null;
        MethodInfo? getRandomMethod = null;
        MethodInfo? getRandomByCharMethod = null;
        object? proverbsRandomizerInstance = null;

        try
        {
            var assembly = Assembly.LoadFrom("ProverbsRandomizer.Library.dll");
            var type = assembly.GetType("ProverbsRandomizer.Library.ProverbsRandomizer");
            proverbsRandomizerInstance = type is null ? null : Activator.CreateInstance(type);
            loadMethod = type?.GetMethod("Load", BindingFlags.Instance | BindingFlags.Public);
            getRandomMethod = type?.GetMethod("GetRandom", BindingFlags.Instance | BindingFlags.Public, Array.Empty<Type>());
            getRandomByCharMethod = type?.GetMethod("GetRandom", BindingFlags.Instance | BindingFlags.Public, new Type[] {typeof(char)});
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

        if (proverbsRandomizerInstance is null 
            || loadMethod is null || getRandomMethod is null || getRandomByCharMethod is null)
        {
            Console.WriteLine("Не найден файл библиотеки или требуемые типы/методы...");
            Console.ReadLine();
            return;
        }



        Console.WriteLine("Приложение выдаёт случайную русскую пословицу или поговорку.\r\nНажмите * или первую букву пословицы для поиска\r\n");

        loadMethod.Invoke(proverbsRandomizerInstance, null);

        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine("Запрос пуст!");
                continue;
            }

            var character = line[0];

            if (character == '*')
            {
                GetAndPrintAny(proverbsRandomizerInstance, getRandomMethod);
            }
            else if (character is (>= 'А' and <= 'я') or 'ё' or 'Ё')
            {
                GetAndPrintByLetter(proverbsRandomizerInstance, getRandomByCharMethod, character);
            }
            else
            {
                Console.WriteLine("Что за буква диковинная? Таковой нет в русском алфавите!");
            }
        }
    }


    private static void GetAndPrintAny(object proverbsRandomizer, MethodInfo method)
    {
        var proverb = method.Invoke(proverbsRandomizer, null);
        if (proverb is null)
        {
            Console.WriteLine("Упс. Найдётся всё. Со временем.");
            return;
        }

        Console.WriteLine("Вот случайная пословица:");
        Console.WriteLine(proverb);
    }

    private static void GetAndPrintByLetter(object proverbsRandomizer, MethodInfo method, char firstChar)
    {
        var proverb = method.Invoke(proverbsRandomizer, new object?[]{firstChar});
        if (proverb is null)
        {
            Console.WriteLine($"Упс, не нашлось пословиц на букву {firstChar}");
            return;
        }

        Console.WriteLine($"Вот случайная пословица на букву {firstChar}:");
        Console.WriteLine(proverb);
    }
}