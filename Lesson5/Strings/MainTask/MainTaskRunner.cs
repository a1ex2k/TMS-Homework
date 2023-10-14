using ConsoleMenu;

namespace Strings;

public class MainTaskRunner
{
    private const string DefaultFile = "default.txt";
    private string _text;
    
    public static void Run()
    {
        new MainTaskRunner().RunInstance();
    }

    private Menu BuildMenu()
    {
        var menu = new Menu("Select an operation with string:");
        menu.SetItem('1', "Words with max digits count", FindWordsWithMaxDigits);
        menu.SetItem('2', "Find longest word", FindLongestWord);
        menu.SetItem('3', "Replace digits with words", ReplaceDigitsWithWords);
        menu.SetItem('4', "Display ? and ! sentences", DisplaySentences);
        menu.SetItem('5', "Display sentences without comma", DisplaySentencesWithoutCommas);
        menu.SetItem('6', "Find words with same first and last letter", FindWordsWithSameFirstAndLastLetter);
        return menu;
    }
    
    private static void PrintList(IList<string> strings)
    {
        foreach (var str in strings)
        {
            Console.WriteLine(str);
        }
    } 

    private void RunInstance()
    {
        Console.WriteLine($"Enter non-empty string or press 'Enter' to load from {DefaultFile}:");
        var text = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Loading from file...");
            try
            {
                text = File.ReadAllText(DefaultFile);
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred while reading the file. 'Enter' to exit");
                Console.ReadLine();
                return;
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine($"Empty text file. 'Enter' to exit");
                Console.ReadLine();
                return;
            }
        }

        _text = text;
        BuildMenu().Run(() => Console.WriteLine($"{text}\r\n"));
    }

    private void FindWordsWithMaxDigits()
    {
        PrintList(StringOperations.FindWordsWithMaxDigits(_text));
    }

    private void FindLongestWord()
    {
        var word = StringOperations.FindLongestWord(_text);
        Console.WriteLine($"Word: {word.Word}, Count: {word.Count}");
    }

    private void ReplaceDigitsWithWords()
    {
        var text = StringOperations.ReplaceDigitsWithWords(_text);
        Console.WriteLine(text);
    }

    private void DisplaySentences()
    {
        PrintList(StringOperations.GetSentences(_text, "?"));
        PrintList(StringOperations.GetSentences(_text, "!"));
    }

    private void DisplaySentencesWithoutCommas()
    {
        PrintList(StringOperations.GetSentencesWithoutCommas(_text));
    }

    private void FindWordsWithSameFirstAndLastLetter()
    {
        PrintList(StringOperations.FindWordsWithSameFirstAndLastLetter(_text));
    }

}