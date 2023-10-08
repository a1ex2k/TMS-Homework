using System.Globalization;
using System.Text;

namespace Calculator;

public class BasicCalculator
{
    private string _expression;
    private double _result = double.NaN;
    private List<string> _subresults = new();
    private bool _isEvaluated = false;
    private bool _hasErrors = false;


    public double? Result => _result;
    public IReadOnlyList<string> Subresults => _subresults.AsReadOnly();
    public bool IsCalculated => _isEvaluated;
    public bool HasErrors => _hasErrors;


    public BasicCalculator(string expression)
    {
        var sb = new StringBuilder(expression.Length);
        for (int i = 0; i < expression.Length; i++)
        {
            if (!char.IsWhiteSpace(expression[i])) 
                sb.Append(expression[i]);
        }

        _expression = sb.ToString();
    }

    
    public double Calculate()
    {
        if (_isEvaluated)
        {
            return _result;
        }

        EvaluateExpression();
        _isEvaluated = true;
        return _result;
    }


    private void EvaluateExpression()
    {
        var operators = new Stack<Operator>();
        var values = new Stack<double>();

        var expression = _expression;

        for (int charIndex = expression.Length - 1; charIndex >= 0;)
        {
            var nonDigitIndex = PrevNonDigitIndex(expression, charIndex);
            var numberTokenLength = charIndex - nonDigitIndex;

            if (double.TryParse(expression.AsSpan(nonDigitIndex + 1, numberTokenLength), NumberStyles.Any, CultureInfo.InvariantCulture, out var number))
            {
                values.Push(number);
                charIndex = nonDigitIndex;
                continue;
            }

            var op = Operator.FromChar(expression[charIndex]);

            if (op != Operator.None)
            {
                while (operators.Count > 0 && operators.Peek().Priority() >= op.Priority())
                {
                    PerformOperation(operators, values);
                    if (_hasErrors) return;
                }

                operators.Push(op);
                charIndex--;
            }
            else
            {
                _isEvaluated = true;
                _hasErrors = true;
                return;
            }
        }

        while (operators.Count > 0)
        {
            PerformOperation(operators, values);
            if (_hasErrors) return;
        }

        if (values.Count == 1)
        {
            _result = values.Pop();
        }
    }


    private int PrevNonDigitIndex(ReadOnlySpan<char> chars, int end = 0)
    {
        for (int charIndex = end; charIndex >= 0; charIndex--)
        {
            var c = chars[charIndex];
            if (char.IsDigit(c) || c is '.' or ',')
                continue;
            if ((c is '+' or '-') && (charIndex == 0 || !char.IsDigit(chars[charIndex - 1])))
                return  charIndex - 1;
            return charIndex;
        }
        return -1;
    }



    private void PerformOperation(Stack<Operator> operators, Stack<double> values)
    {
        if (operators.Count == 0 || values.Count < 2)
        {
            _hasErrors = true;
            return;
        }
        var op = operators.Pop();
        var rightOperand = values.Pop();
        var leftOperand = values.Pop();
        
        var evaluated = op.EvaluateAsString(rightOperand, leftOperand);

        values.Push(evaluated.ResultValue);
        _subresults.Add(evaluated.String);
    }
}