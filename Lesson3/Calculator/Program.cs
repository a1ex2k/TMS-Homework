using Calculator;

Console.WriteLine("Enter expression\r\nAllowed operators:  '+', '-', '*', '/', '^', '%' (percent), '#' (root).\r\nParenteses and number with exponent not alowwed.Enter 'q' to quit.");

while (true)
{
    Console.Write("\n> ");
    var line = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(line))
    {
        Console.WriteLine("Input was empty. Try again.");
        continue;
    }
    else if (string.Equals(line, "q", StringComparison.OrdinalIgnoreCase))
    {
        return;
    }

    var calculator = new BasicCalculator(line);
    var result = calculator.Calculate();

    var resultLine = calculator.HasErrors ? "Expression cannot be parsed. Try again." : $"Answer: {result}";
    Console.WriteLine(resultLine);

    if (calculator.Subresults.Count > 1) 
    {
        Console.WriteLine($"Steps:  {calculator.Subresults[0]}");
        for (int i = 1; i < calculator.Subresults.Count; i++ )
        {
            Console.WriteLine($"        {calculator.Subresults[i]}");
        }
    }

}

