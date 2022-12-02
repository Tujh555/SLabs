using System.Diagnostics;

namespace SLabs.Lab2;

public class Program
{
    private static readonly string[] _menuItems = new[]
    {
        "1. Начать ввод",
        "2. Выход"
    };

    private static bool _isInputInProcess = false;

    public static void Main1(string[] args)
    {
        while (true)
        {
            if (_isInputInProcess)
            {
                DrawUserInput();
            }
            else
            {
                DrawMenu();
            }
        }
    }

    private static void SelectMenuItem()
    {
        Console.WriteLine("Выберите пункт меню: ");
        var item = Console.ReadLine();

        if (item != "1" && item != "2")
        {
            Console.WriteLine($"Такого пункта меню не существует: {item}");
            return;
        }

        if (item == "1")
        {
            _isInputInProcess = true;
        }
        else
        {
            Process.GetCurrentProcess().Kill();
        }
    }

    private static void DrawUserInput()
    {
        Console.Clear();
        Console.WriteLine("1) Введите модуль числа и его степень через пробел");

        var line = Console.ReadLine() ?? "1 1";
        (double module, double degree) first;

        try
        {
            first = Reader.GetComplexData(line);
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Пожалуйста, введите две цифры");
            return;
        }
        
        Console.WriteLine("2) Введите модуль числа и его степень через пробел");

        line = Console.ReadLine() ?? "1 1";
        (double module, double degree) second;

        try
        {
            second = Reader.GetComplexData(line);
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Пожалуйста, введите две цифры");
            return;
        }
        
        Console.WriteLine("3) Введите операцию, которую нужно провести над данными числами");
        var arithmeticOperation = Console.ReadLine()?.First() ?? '+';

        Operation? operation;

        try
        {
            operation = Operation.Builder
                .Operation(arithmeticOperation)
                .FirstDegree(first.degree)
                .FirstModule(first.module)
                .SecondDegree(second.degree)
                .SecondModule(second.module)
                .Build();
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Неизвестная арифметическая операция");
            return;
        }

        if (operation == null)
        {
            Console.WriteLine("Введены не все параметры!");
            return;
        }

        var result = operation.GetResult();
        
        Console.WriteLine($"{operation.FirstNumber} {operation.ArithmeticOperation} {operation.SecondNumber} = {result}");
        
        Console.WriteLine("Чтобы выйти, нажмите A, чтобы продолжить - любую другую кнопку");

        if (Console.ReadKey().Key == ConsoleKey.A)
        {
            _isInputInProcess = false;
        }
    }
    
    private static void DrawMenu()
    {
        Console.Clear();
        foreach (var item in _menuItems)
        {
            Console.WriteLine(item);
        }
        
        SelectMenuItem();
    }
}