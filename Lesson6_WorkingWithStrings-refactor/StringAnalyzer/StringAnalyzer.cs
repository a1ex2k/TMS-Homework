using System.Text;

namespace WorkingWithStrings;

public class StringAnalyzer
{
    private static readonly char[] WordSeparators = new[] { ' ', '.', ',', '!', '?', ':', ';', '\t', '\n', '\r' };
    private static readonly char[] SentenceSeparators = new[] { '.', '!', '?' };

    private static readonly string[] DigitsAsWords = new[]
    {
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
    };

    private readonly string _text;


    public StringAnalyzer(string text)
    {
        _text = text;
    }


    public IList<string> SplitIntoWords(string text)
    {
        return text.Split(WordSeparators, StringSplitOptions.RemoveEmptyEntries);
    }


    public IList<string> SplitIntoSentences()
    {
        var startIndex = 0;
        var sentences = new List<string>();

        while (startIndex < _text.Length)
        {
            var endIndex = _text.IndexOfAny(SentenceSeparators, startIndex);
            if (endIndex == -1)
            {
                endIndex = _text.Length;
            }
            if (endIndex == startIndex + 1)
            {
                continue;
            }
            var sentenceLength = endIndex - startIndex + 1;
            var sentence = _text.Substring(startIndex, sentenceLength).Trim();
            sentences.Add(sentence);

            startIndex = endIndex + 1;
        }
        return sentences.ToArray();
    }

    public IList<string> FindWordsWithMaxDigits()
    {
        var words = SplitIntoWords(_text);
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

    public NumberAndWordPair FindLongestWord()
    {
        var words = SplitIntoWords(_text);

        var longestWord = words.MaxBy(w => w.Length)!;
        var longestWordCount = words.Count(w => string.Equals(w, longestWord, StringComparison.InvariantCultureIgnoreCase));

        return new(longestWord, longestWordCount);
    }

    public string ReplaceDigitsWithWords()
    {
        var resultBuilder = new StringBuilder(_text.Length);
        foreach (char c in _text)
        {
            if (char.IsDigit(c))
            {
                resultBuilder.Append(DigitsAsWords[c - '0']);
            }
            else
            {
                resultBuilder.Append(c);
            }
        }

        return resultBuilder.ToString();
    }

    public IList<string> GetSentences(string punctuation)
    {
        var sentences = SplitIntoSentences();
        var filtered = sentences.Where(s => s.EndsWith(punctuation)).ToList();
        return filtered;
    }

    public IList<string> GetSentencesWithout(string punctuation)
    {
        var sentences = SplitIntoSentences();
        var withoutCommas = sentences.Where(s => !s.Contains(punctuation)).ToList();
        return withoutCommas;
    }

    public IList<string> FindWordsWithSameFirstAndLastLetter()
    {
        var sentences = SplitIntoSentences();
        var withSameLetters = sentences.Where(s => char.ToLower(s[0]) == char.ToLower(s[^1])).ToList();
        return withSameLetters;
    }
}