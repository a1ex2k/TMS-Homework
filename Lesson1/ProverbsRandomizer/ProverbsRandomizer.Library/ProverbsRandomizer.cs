using System.Diagnostics;
using System.Reflection;

namespace ProverbsRandomizer.Library;

public class ProverbsRandomizer
{
    private readonly Random _random = Random.Shared;

    private const char CharA = 'а';
    private const char CharYa = 'я';
    private const string ResourceName = "ProverbsRandomizer.Library.Proverbs.txt";

    private Dictionary<char, List<string>?> _proverbsDictionary = new();
    private bool _isLoaded = false;


    public void Load()
    {
        var newDictionary = new Dictionary<char, List<string>?>();
        var assembly = Assembly.GetExecutingAssembly();

        try
        {
            using var resourceStream = assembly.GetManifestResourceStream(ResourceName);
            using var reader = new StreamReader(resourceStream);

            string? line;
            while ((line = reader?.ReadLine()) is not null)
            {
                var keyChar = char.ToLower(line[0]);
                if (!newDictionary.TryGetValue(keyChar, out var list) || list is null)
                {
                    newDictionary[keyChar] = new List<string>();
                }

                newDictionary[keyChar]!.Add(line);
            }
            
            _proverbsDictionary = newDictionary;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

        
        _isLoaded = true;
    }

    
    public string? GetRandom()
    {
        if (!_isLoaded)
        {
            Load();
        }

        var randomKey = _proverbsDictionary.Keys.ElementAt(_random.Next(_proverbsDictionary.Keys.Count));
        TryGetProverb(randomKey, out var proverb);
        return proverb;
    }

    
    public string? GetRandom(char firstLetter)
    {
        if (!_isLoaded)
        {
            Load();
        }

        return TryGetKeyFromLetter(firstLetter, out var key) && TryGetProverb(key, out var proverb) ? proverb : null;
    }


    private bool TryGetKeyFromLetter(char firstLetter, out char key)
    {
        key = default;
        var lowered = char.ToLower(firstLetter);

        if (lowered is < CharA or > CharYa)
        {
            return false;
        }

        key = lowered;
        return true;
    }


    private bool TryGetProverb(char key, out string proverb)
    {
        proverb = default!;
        if (!_proverbsDictionary.TryGetValue(key, out var listByKey)
            || listByKey is null
            || listByKey.Count == 0)
        {
            return false;
        }

        proverb = listByKey[_random.Next(listByKey.Count)];
        return true;
    }

}