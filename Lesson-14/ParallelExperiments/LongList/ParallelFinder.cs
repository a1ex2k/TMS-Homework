namespace LongList;

public class ParallelFinder<TItem>
{
    private Func<TItem, bool> _predicate;
    private IList<TItem> _list;
    private Action<TItem> _onFoundAction;
    private readonly int _degreeOfParallelism;

    public ParallelFinder(IList<TItem> list, Func<TItem, bool> predicate, Action<TItem> onFoundAction)
        : this (list, predicate, onFoundAction, Environment.ProcessorCount - 1)
    { }


    public ParallelFinder(IList<TItem> list, Func<TItem, bool> predicate, Action<TItem> onFoundAction, int degreeOfParallelism)
    {
        _list = list;
        _predicate = predicate;
        _onFoundAction = onFoundAction;
        _degreeOfParallelism = degreeOfParallelism < 1 ? 1 : degreeOfParallelism;
    }


    public void BeginSearch(CancellationToken cancellationToken = new CancellationToken())
    {
        var chunkSize = (_list.Count - 1) / _degreeOfParallelism + 1;

        for (int i = 0; i < _degreeOfParallelism; i++)
        {
            var chunkIndex = i;
            var thread = new Thread(() =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var fromIndex = chunkSize * chunkIndex;
                var endIndex = Math.Min(chunkSize * (chunkIndex + 1), _list.Count);
                FindInThread(_list, fromIndex, endIndex, cancellationToken);
            });
            thread.Start();
        }
    }



    private void FindInThread(IList<TItem> list, int startIndex, int endIndex, CancellationToken cancellationToken)
    {
        for (int i = startIndex; i < endIndex; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (_predicate(list[i]))
            {
                _onFoundAction.Invoke(list[i]);
            }
        }
    }


}