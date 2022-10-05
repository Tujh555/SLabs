namespace SLabs.Lab2;

public class Operation
{
    public readonly ComplexNumber FirstNumber;
    public readonly ComplexNumber SecondNumber;
    public readonly char ArithmeticOperation;
    public static OperationBuilder Builder => new();

    private Operation(ComplexNumber firstNumber, ComplexNumber secondNumber, char arithmeticOperation)
    {
        FirstNumber = firstNumber;
        SecondNumber = secondNumber;
        ArithmeticOperation = arithmeticOperation;
    }

    public ComplexNumber GetResult() => ArithmeticOperation switch
    {
        '+' => FirstNumber + SecondNumber,
        '-' => FirstNumber - SecondNumber,
        '*' => FirstNumber * SecondNumber,
        _ => new ComplexNumber(0, 0)
    };

    public class OperationBuilder
    {
        private readonly HashSet<char> _validArithmeticOperations = new(new[] { '+', '-', '*' });
        private double? _firstModule, _firstDegree, _secondModule, _secondDegree;
        private char? _arithmeticOperation;

        public OperationBuilder FirstModule(double module)
        {
            _firstModule = module;
            return this;
        }
        
        public OperationBuilder SecondModule(double module)
        {
            _secondModule = module;
            return this;
        }
        
        public OperationBuilder FirstDegree(double degree)
        {
            _firstDegree = degree;
            return this;
        }
        
        public OperationBuilder SecondDegree(double degree)
        {
            _secondDegree = degree;
            return this;
        }

        public OperationBuilder Operation(char operation)
        {
            if (!_validArithmeticOperations.Contains(operation))
            {
                throw new ArgumentException("Unknown arithmetic operation");
            }

            _arithmeticOperation = operation;

            return this;
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