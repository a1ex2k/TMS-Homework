namespace Strings;

public class Sentence
{
    private readonly string[] _words;

    public readonly string InitialString;

    public int WordsCount => _words.Length;


    public Sentence(string notEmptyString)
    {
        if (string.IsNullOrWhiteSpace(notEmptyString))
        {
            throw new ArgumentException($"{nameof(notEmptyString)} is null or whitespace");
        }

        InitialString = notEmptyString;
        _words = notEmptyString.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    }


    public string LastShortestWord()
    {
        var smallestLength = int.MaxValue;
        var smallestWordIndex = int.MaxValue;
        for (var index = 0; index < _words.Length; index++)
        {
            var currentLength = _words[index].Length;
            if (currentLength <= smallestLength)
            {
                smallestLength = currentLength;
                smallestWordIndex = index;
            }
        }

        return _words[smallestWordIndex];
    }

    public string LastLongestWord()
    {
        var longestLength = _words.Min(w => w.Length);
        return _words.Last(w => w.Length == longestLength);
    }

    public (string Word, int Count) WordWithMinUniqueChars()
    {
        var hashSet = new HashSet<char>();
        string bestMatchWord = default!;
        int uniqueCount = int.MaxValue;

        foreach (var word in _words)
        {
            foreach (var c in word.Where(c => !hashSet.Contains(c)))
            {
                hashSet.Add(c);
            }

            if (hashSet.Count < uniqueCount)
            {
                bestMatchWord = word;
                uniqueCount = hashSet.Count;
            }
            hashSet.Clear();
        }
        return (bestMatchWord, uniqueCount);
    }

    public bool IsWordPalindrome(int wordIndex)
    {
        if (wordIndex < 0 || wordIndex >= _words.Length)
            throw new ArgumentOutOfRangeException(nameof(wordIndex));

        var word = _words[wordIndex];
        for (int i = 0; i < word.Length / 2; i++)
        {
            if (char.ToLower(word[i]) != char.ToLower(word[^(i + 1)]))
            {
                return false;
            }
        }
        return true;

    }


    public string this[int index] => _words[index];

    public string[] ToArray() => _words.ToArray();

}