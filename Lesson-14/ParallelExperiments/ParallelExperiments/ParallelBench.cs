using BenchmarkDotNet.Attributes;

namespace ParallelExperiments;

[MemoryDiagnoser]
[MinIterationCount(8)]
[MaxIterationCount(9)]
public class ParallelBench
{
    [Params(2, 3, 4, 5, 6, 7, 8, 9, 10)]
    public int DegreeOfParallelism { get; set; }

    private const int ArrayLength = 10_000_000;
    private readonly Random _random = new Random(666);
    private int[] _array;


    public long SumSingleThread(Span<int> span)
    {
        var sum = 0L;
        for (int i = 0; i < span.Length; i++)
        {
            sum += (long)span[i];
        }

        return sum;
    }

    public long SumAsParallel(int[] array, int degreeOfParallelism)
    {
        return array.AsParallel()
            .WithDegreeOfParallelism(degreeOfParallelism)
            .Select(n => (long)n)
            .Sum();
    }

    public async Task<long> SumWithTaskAsync(int[] array, int degreeOfParallelism)
    {
        var chunkLength = (int)Math.Ceiling(array.Length / (double)degreeOfParallelism);

        var tasks = new Task<long>[degreeOfParallelism];
        for (int i = 0; i < degreeOfParallelism; i++)
        {
            var localIndex = i;
            tasks[i] = new Task<long>(() =>
            {
                var fromIndex = chunkLength * localIndex;
                var toIndex = Math.Min(chunkLength * (localIndex + 1), array.Length);
                var span = array.AsSpan(fromIndex..toIndex);
                return SumSingleThread(span);
            });
            tasks[i].Start();
        }

        var subSum = await Task.WhenAll(tasks);
        var sum = subSum.Sum();
        return sum;
    }


    public long SumWithThreads(int[] array, int degreeOfParallelism)
    {
        var chunkLength = (int)Math.Ceiling(array.Length / (double)degreeOfParallelism);

        var threads = new Thread[degreeOfParallelism];
        var subSum = new long[degreeOfParallelism];

        for (int i = 0; i < threads.Length; i++)
        {
            var localIndex = i;
            threads[i] = new Thread(() =>
            {
                var fromIndex = chunkLength * localIndex;
                var toIndex = Math.Min(chunkLength * (localIndex + 1), array.Length);
                var span = array.AsSpan(fromIndex..toIndex);
                subSum[localIndex] = SumSingleThread(span);
            });
            threads[i].Start();
        }

        for (int i = 0; i < threads.Length; i++)
        {
            threads[i].Join();
        }

        var sum = subSum.Sum();
        return sum;
    }


    [GlobalSetup]
    public void Setup()
    {
        _array = new int[ArrayLength];
        for (int i = 0; i < ArrayLength; i++)
        {
            _array[i] = _random.Next(-10_000, 10_000);
        }
    }


    [Benchmark]
    public void SumSingleThread() => SumSingleThread(_array);

    [Benchmark]
    public void SumAsParallel() => SumAsParallel(_array, DegreeOfParallelism);

    [Benchmark]
    public async Task SumWithTaskAsync() => await SumWithTaskAsync(_array, DegreeOfParallelism);

    [Benchmark]
    public void SumWithThreads() => SumWithThreads(_array, DegreeOfParallelism);
}