namespace PairOf;

public class ComparablePair<TFirst, TSecond> : IPair<TFirst, TSecond>, IComparable<ComparablePair<TFirst, TSecond>>
    where TFirst : IComparable<TFirst>
    where TSecond : IComparable<TSecond>
{
    private TFirst _first;
    private TSecond _second;
    
    public TFirst First
    {
        get => _first;
        init => _first = value;
    }

    public TSecond Second
    {
        get => _second;
        init => _second = value;
    }

    public ComparablePair(TFirst first, TSecond second)
    {
        _first = first;
        _second = second;
    }

    public void UpdateFirst(TFirst newValue)
    {
        _first = newValue;
    }

    public void UpdateSecond(TSecond newValue)
    {
        _second = newValue;
    }

    public object? this[int index] =>
        index switch
        {
            0 => _first,
            1 => _second,
            _ => throw new IndexOutOfRangeException(),
        };

    public int CompareTo(ComparablePair<TFirst, TSecond>? other)
    {
        if (other is null)
        {
            return 1;
        }

        var firstCompare = CompareInternal(_first, other._first);
        if (firstCompare != 0)
        {
            return firstCompare;
        }

        return CompareInternal(_second, other._second);
    }

    private int CompareInternal<T>(T first, T second) where T : IComparable<T>
    {
        if (first is not null)
        {
            return first.CompareTo(second);
        }

        if (second is not null)
        {
            return -second.CompareTo(first);
        }

        return 0;
    }

}