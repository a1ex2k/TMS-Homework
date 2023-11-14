using LongList;

Console.WriteLine("Hello, World!");

var list = Enumerable.Range(0, 1000000).ToList();
var predicate = (int number) => number % 789 == 0;
var onFoundAction = (int number) =>
{
    Console.WriteLine($"TreadId: {Thread.CurrentThread.ManagedThreadId}\t Item: {number}");
};


var cts = new CancellationTokenSource();
Console.CancelKeyPress += (sender, eventArgs) =>
{
    Console.WriteLine("Cancel event triggered");
    cts.Cancel();
    eventArgs.Cancel = true;
};

Console.WriteLine("Return to start, Ctrl+C to cancel");
Console.ReadLine();

await list.ForMatchingAsync(predicate, onFoundAction, cts.Token);

Console.ReadKey();