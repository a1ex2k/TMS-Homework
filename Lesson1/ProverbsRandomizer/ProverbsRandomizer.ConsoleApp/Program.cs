namespace ProverbsRandomizer.ConsoleApp;
internal class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Приложение выдаёт случайную русскую пословицу или поговорку.\r\nНажмите * или первую букву пословицы для поиска\r\n");

        var proverbsRandomizer = new Library.ProverbsRandomizer();
        proverbsRandomizer.Load();

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
                GetAndPrintAny(proverbsRandomizer);
            }
            else if (character is (>= 'А' and <= 'я') or 'ё' or 'Ё')
            {
                GetAndPrintByLetter(proverbsRandomizer, character);
            }
            else
            {
                Console.WriteLine("Что за буква диковинная? Таковой нет в русском алфавите!");
            }
        }
    }


    private static void GetAndPrintAny(Library.ProverbsRandomizer proverbsRandomizer)
    {
        var proverb = proverbsRandomizer.GetRandom();
        if (proverb is null)
        {
            Console.WriteLine("Упс. Найдётся всё. Со временем.");
            return;
        }

        Console.WriteLine("Вот случайная пословица:");
        Console.WriteLine(proverb);
    }

    private static void GetAndPrintByLetter(Library.ProverbsRandomizer proverbsRandomizer, char firstChar)
    {
        var proverb = proverbsRandomizer.GetRandom(firstChar);
        if (proverb is null)
        {
            Console.WriteLine($"Упс, не нашлось пословиц на букву {firstChar}");
            return;
        }

        Console.WriteLine($"Вот случайная пословица на букву {firstChar}:");
        Console.WriteLine(proverb);
    }
}
