namespace LongList;

public static class IListParallelExtensions
{
    public static async Task ForMatchingAsync<TItem>(this IList<TItem> list, Func<TItem, bool> predicate, Action<TItem> onMatch, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(predicate);
        ArgumentNullException.ThrowIfNull(onMatch);

        var parallelFinder = new ParallelFinder<TItem>(list, predicate, onMatch);
        await parallelFinder.SearchAsync(cancellationToken);
    }
    
}