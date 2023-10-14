using System.Text;

namespace Strings;

public static class StringOperations
{
    private static readonly char[] WordSeparators = new[] { ' ', '.', ',', '!', '?', ':', ';', '\t', '\n', '\r'};
    private static readonly char[] SentenceSeparators = new[] { '.', '!', '?' };

    private static Dictionary<char, string> DigitWordMap = new()
    {
        {'0', "zero"},
        {'1', "one"},
        {'2', "two"},
        {'3', "three"},
        {'4', "four"},
        {'5', "five"},
        {'6', "six"},
        {'7', "seven"},
        {'8', "eight"},
        {'9', "nine"}
    };

    public static IList<string> SplitIntoWords(string text)
    {
        return text.Split(WordSeparators, StringSplitOptions.RemoveEmptyEntries);
    }

    public static IList<string> SplitIntoSentences(string text)
    {
        var startIndex = 0;
        var sentences = new List<string>();

        while (startIndex < text.Length)
        {
            var endIndex = text.IndexOfAny(SentenceSeparators, startIndex);
            if (endIndex == -1)
            {
                endIndex = text.Length;
            }
            if (endIndex == startIndex + 1)
            {
                continue;
            }
            var sentenceLength = endIndex - startIndex + 1;
            var sentence = text.Substring(startIndex, sentenceLength).Trim();
            sentences.Add(sentence);

            startIndex = endIndex + 1;
        }
        return sentences.ToArray();
    }

    public static IList<string> FindWordsWithMaxDigits(string text)
    {
        var words = SplitIntoWords(text);
        var maxDigitCount = 0;
        var digitCounts = new int[words.Count];

        for (var wordIndex = 0; wordIndex < words.Count; wordIndex++)
        {
            var word = words[wordIndex];
            var digitCount = 0;
            for (int charIndex = 0; charIndex < word.Length; charIndex++)
            {
                digitCount += char.IsDigit(word[charIndex]) ? 1 : 0;
            }

            digitCounts[wordIndex] = digitCount; 

            if (digitCount > maxDigitCount)
            {
                maxDigitCount = digitCount;
            }
        }

        var list = new List<string>();

        for (var index = 0; index < words.Count; index++)
        {
            var word = words[index];
            if (digitCounts[index] == maxDigitCount)
            {
                list.Add(word);
            }
        }
        return list;
    }
    
    public static (string Word, int Count) FindLongestWord(string text)
    {
        var words = SplitIntoWords(text);

        var longestWord = words.MaxBy(w => w.Length)!;
        var longestWordCount = words.Count(w => string.Equals(w, longestWord, StringComparison.InvariantCultureIgnoreCase));

        return (longestWord, longestWordCount);
    }

    public static string ReplaceDigitsWithWords(string text)
    {
        StringBuilder result = new StringBuilder(text.Length);

        foreach (char c in text)
        {
            if (DigitWordMap.TryGetValue(c, out string word))
            {
                result.Append(word);
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    public static IList<string> GetSentences(string text, string punctuation)
    {
        var sentences = SplitIntoSentences(text);
        var filtered = sentences.Where(s => s.EndsWith(punctuation)).ToList();
        return filtered;
    }

    public static IList<string> GetSentencesWithoutCommas(string text)
    {
        var sentences = SplitIntoSentences(text);
        var withoutCommas = sentences.Where(s => !s.Contains(',')).ToList();
        return withoutCommas;
    }

    public static IList<string> FindWordsWithSameFirstAndLastLetter(string text)
    {
        var sentences = SplitIntoSentences(text);
        var withSameLetters = sentences.Where(s => char.ToLower(s[0]) == char.ToLower(s[^1]) ).ToList();
        return withSameLetters;
    }
}