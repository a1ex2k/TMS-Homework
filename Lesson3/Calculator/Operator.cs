namespace Calculator;

public enum OperatorType
{
    None, Plus, Minus, Multiply, Divide, Percent, Power, Root
}


public readonly struct Operator
{
    public static Operator None = new Operator(OperatorType.None);

    public readonly OperatorType Type;

    private Operator(OperatorType type)
    {
        Type = type;
    }


    public static Operator FromChar(char inputChar)
    {
        return inputChar switch
        {
            '+' => new Operator(OperatorType.Plus),
            '-' => new Operator(OperatorType.Minus),
            '*' => new Operator(OperatorType.Multiply),
            '/' => new Operator(OperatorType.Divide),
            '%' => new Operator(OperatorType.Percent),
            '#' => new Operator(OperatorType.Root),
            '^' => new Operator(OperatorType.Power),
            _ => None
        };
    }


    public int Priority()
    {
        switch (Type)
        {
            case OperatorType.Plus:
            case OperatorType.Minus:
                return 1;
            case OperatorType.Multiply:
            case OperatorType.Divide:
                return 2;
            case OperatorType.Power:
            case OperatorType.Percent:
            case OperatorType.Root:
                return 3;
            default:
                return int.MaxValue;
        }
    }


    public double Evaluate(double firstOperand, double secondOperand = double.NaN)
    {
        return Type switch
        {
            OperatorType.None => double.NaN,
            OperatorType.Plus => firstOperand + secondOperand,
            OperatorType.Minus => firstOperand - secondOperand,
            OperatorType.Multiply => firstOperand * secondOperand,
            OperatorType.Divide => firstOperand / secondOperand,
            OperatorType.Percent => firstOperand * secondOperand / 100.0,
            OperatorType.Power => Math.Pow(firstOperand, secondOperand),
            OperatorType.Root => Math.Sqrt(firstOperand),
            _ => double.NaN,
        };
    }


    public (double ResultValue, string String) EvaluateAsString(double firstOperand, double secondOperand = double.NaN)
    {
        var result = Evaluate(firstOperand, secondOperand);

        string stringResult = string.Empty;

        if (Type == OperatorType.Root)
            stringResult = $"root({firstOperand}, {secondOperand}) = {result}";
        else if (Type == OperatorType.Percent)
            stringResult = $"{firstOperand} * {secondOperand}% = {result}";
        else 
            stringResult = $"{firstOperand} {this} {secondOperand} = {result}";

        return (result, stringResult);
    }


    public override string ToString()
    {
        return Type switch
        {
            OperatorType.None => "<None>",
            OperatorType.Plus => "+",
            OperatorType.Minus => "-",
            OperatorType.Multiply => "*",
            OperatorType.Divide => "/",
            OperatorType.Percent => "%",
            OperatorType.Power => "^",
            OperatorType.Root => "root",
            _ => "<None>"
        };
    }


    public static bool operator ==(Operator op1, Operator op2)
    {
        return op1.Type == op2.Type;
    }

    public static bool operator !=(Operator op1, Operator op2)
    {
        return op1.Type != op2.Type;
    }

}
