namespace PairOf;

public class Pair<TFirst, TSecond> : IPair<TFirst, TSecond>
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

    public Pair(TFirst first, TSecond second)
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
}