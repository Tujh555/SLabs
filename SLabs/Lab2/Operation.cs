namespace SLabs.Lab2;

public class Operation
{
    private readonly ComplexNumber _firstNumber;
    private readonly ComplexNumber _secondNumber;
    private readonly char _arithmeticOperation;
    private OperationBuilder Builder => new();

    private Operation(ComplexNumber firstNumber, ComplexNumber secondNumber, char arithmeticOperation)
    {
        _firstNumber = firstNumber;
        _secondNumber = secondNumber;
        _arithmeticOperation = arithmeticOperation;
    }

    public ComplexNumber GetResult() => _arithmeticOperation switch
    {
        '+' => _firstNumber + _secondNumber,
        '-' => _firstNumber - _secondNumber,
        '*' => _firstNumber * _secondNumber,
        _ => new ComplexNumber(0, 0)
    };

    private class OperationBuilder
    {
        private readonly HashSet<char> _validArithmeticOperations = new(new[] { '+', '-', '*' });
        private double? _firstModule, _firstDegree, _secondModule, _secondDegree;
        private char? _arithmeticOperation;

        public void FirstModule(double module)
        {
            _firstModule = module;
        }
        
        public void SecondModule(double module)
        {
            _secondModule = module;
        }
        
        public void FirstDegree(double degree)
        {
            _firstDegree = degree;
        }
        
        public void SecondDegree(double degree)
        {
            _secondDegree = degree;
        }

        public void Operation(char operation)
        {
            if (!_validArithmeticOperations.Contains(operation))
            {
                throw new ArgumentException("Unknown arithmetic operation");
            }

            _arithmeticOperation = operation;
        }

        public Operation? Build()
        {
            if (_firstModule is null
                || _firstDegree is null
                || _secondDegree is null
                || _secondModule is null
                || _arithmeticOperation is null)
            {
                return null;
            }

            var first = new ComplexNumber((double)_firstModule, (double)_firstDegree);
            var second = new ComplexNumber((double)_secondModule, (double)_secondDegree);
            var operation = (char)_arithmeticOperation;

            return new Operation(first, second, operation);
        }
    }
}