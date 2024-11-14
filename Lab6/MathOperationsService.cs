using System;

public static class MathOperationsService
{
    private static readonly Dictionary<char, (byte, Func<double, double, double>)> _binaryOperators = new Dictionary<char, (byte, Func<double, double, double>)>();
    private static readonly char[] _numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
    static MathOperationsService()
    {
        _binaryOperators.Add('+', (0, (double a, double b) => a + b));
        _binaryOperators.Add('-', (0, (double a, double b) => a - b));
        _binaryOperators.Add('/', (1, (double a, double b) => a / b));
        _binaryOperators.Add('*', (1, (double a, double b) => a * b));
        _binaryOperators.Add('%', (1, (double a, double b) => a % b));
        _binaryOperators.Add('^', (2, (double a, double b) => Math.Pow(a, b)));
    }

    public static string ToRPN(this string originExpression)
    {
        string outputString = string.Empty;

        LinkedList<char> stack = new LinkedList<char>();

        foreach (char c in originExpression)
        {
            if (_numbers.Contains(c))
                outputString += c;
            else if (c == '(')
                stack.AddLast('(');
            else if (c == ')')
            {
                while (stack.Last() != '(')
                {
                    outputString += stack.Last();
                    stack.RemoveLast();
                    if (stack.Count() == 0)
                    {
                        throw new InvalidDataException("В исходном выражении имеются несогласованные скобки!");
                    }
                }
                stack.RemoveLast();
            }
            else if (_binaryOperators.Keys.Contains(c))
            {
                while (stack.Count() > 0 && stack.Last() != '(' && _binaryOperators[c].Item1 <= _binaryOperators[stack.Last()].Item1)
                {
                    outputString += stack.Last();
                    stack.RemoveLast();
                }
                outputString += ' ';
                stack.AddLast(c);
            }
            else if (c == ' ')
                outputString += c;
            else
            {
                throw new InvalidDataException("В исходном выражении имеются недопустимые символы!");
            }
        }

        foreach (var token in stack.Reverse())
        {
            outputString += token;
        }

        return outputString;
    }

    public static double Calculate(string inputRPN)
    {
        inputRPN = inputRPN.Replace('.', ',');
        var stack = new Stack<double>();

        string numberBuffer = string.Empty;
        foreach (char c in inputRPN)
        {
            if (_numbers.Contains(c))
                numberBuffer += c;
            else if (c == ' ' && !string.IsNullOrEmpty(numberBuffer))
            {
                stack.Push(Convert.ToDouble(numberBuffer));
                numberBuffer = string.Empty;
            }
            else if (_binaryOperators.Keys.Contains(c))
            {
                if (!string.IsNullOrEmpty(numberBuffer))
                {
                    stack.Push(Convert.ToDouble(numberBuffer));
                    numberBuffer = string.Empty;
                }
                var b = stack.Pop();
                var a = stack.Pop();
                stack.Push(_binaryOperators[c].Item2(a, b));
            }
        }
        return stack.Pop();
    }
}
