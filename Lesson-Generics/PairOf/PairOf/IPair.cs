namespace PairOf;

public interface IPair<TFirst, TSecond>
{
    TFirst First { get; init; }

    TSecond Second { get; init; }

    void UpdateFirst(TFirst newValue);

    void UpdateSecond(TSecond newValue);

    object? this[int index] { get; }
}