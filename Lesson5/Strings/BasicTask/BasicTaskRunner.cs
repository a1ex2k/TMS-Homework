namespace Strings;

public static class BasicTaskRunner
{
    public static void Run()
    {
        var documentNumbers = new[]
        {
            "5555-@bc-rrrr-333-1234",
            "1234-abC-6789-jtl-2s2s",
            "5450-AuC-8248-sez-1a2b",
            "5550-ygj-2587-abc-1a2b",
        };

        var sentences = new[]
        {
            "Lorel ipsum dolod sit amet",
            "Сonsectetur adipiscing elit",
            "Pellentesque de nuqun condimentum ablatir at dictum",
            " \r \t ",
            "Ed pharetra massa eu dapibus laoreet",
        };


        foreach (var number in documentNumbers)
        {
            RunDocumentNumberTask(number);
        }

        for (var index = 0; index < sentences.Length; index++)
        {
            RunSentenceTask(sentences[index], () => index);
        }

        foreach (var line in sentences)
        {
            RunDuplicateTask(line);
        }
    }

    public static void RunDocumentNumberTask(string documentNumber)
    {
        Console.WriteLine($"\r\n// Document number // {documentNumber}");

        if (DocumentNumber.IsValid(documentNumber))
        {
            Console.WriteLine(DocumentNumber.GetDigitBlocks(documentNumber));
            Console.WriteLine(DocumentNumber.ReplaceNumberBlocksWithAsterisks(documentNumber));
            Console.WriteLine(DocumentNumber.LettersLowerCase(documentNumber));
            Console.WriteLine(DocumentNumber.LettersUpperCase(documentNumber));
            Console.WriteLine($"Contains 'abc': {DocumentNumber.ContainsAbc(documentNumber)}");
            Console.WriteLine($"Starts with '555': {DocumentNumber.StartsWith555(documentNumber)}");
            Console.WriteLine($"Ends with '1a2b': {DocumentNumber.EndsWith1a2b(documentNumber)}");
        }
        else
        {
            Console.WriteLine("Not valid");
        }
    }

    public static void RunSentenceTask(string line, Func<int> indexRetriever)
    {
        Console.WriteLine($"\r\n// Sting of words // {line}");
        if (string.IsNullOrWhiteSpace(line))
        {
            Console.WriteLine("Empty string passed");
            return;
        }

        var sentence = new Sentence(line);

        Console.WriteLine($"Last shortest word: {sentence.LastShortestWord()}");
        Console.WriteLine($"Last longest word: {sentence.LastLongestWord()}");
        Console.WriteLine($"Word with minimum of unique characters: {sentence.WordWithMinUniqueChars()}");

        int index = indexRetriever();
        if (index <= 0 || index >= sentence.WordsCount)
        {
            Console.WriteLine($"Index {index} is not in range [0, {sentence.WordsCount - 1}]");
        }

        Console.WriteLine($"Is ([{index}] == {sentence[index]}) a palindrome: {sentence.IsWordPalindrome(index)}");
    }

    public static void RunDuplicateTask(string line)
    {
        Console.WriteLine("\r\n// Duplicate characters task");
        if (string.IsNullOrWhiteSpace(line))
        {
            Console.WriteLine("Empty string passed");
            return;
        }

        var result = line.DuplicateCharacters();
        Console.WriteLine($"Result: {result}");
    }
}