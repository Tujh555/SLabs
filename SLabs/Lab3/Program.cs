using System.Diagnostics;
using SLabs.Lab3.Employees;

namespace SLabs.Lab3;

public static class Program
{
    private static readonly string[] MenuItems =
    {
        "1.Сортировать сотрудников",
        "2.Выести всех сотрудников",
        "3.Вывести первые 6 имён сотрудников",
        "4.Вывести последние 4 идентификатора сотрудников",
        "5.Записать в файл",
        "6.Получить организацию из файла",
        "7.Добавить сотрудника",
        "8.Выход",
    };

    private static string[] _hourlyEmployees = new[]
    {
        "Менеджер",
        "Учитель",
        "Редактор"
    };

    private static Organization _organization = new();
    private static bool _isInputInProcess;
    private static int _currId;

    public static void Main(string[] args)
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

                Console.WriteLine("Чтобы продолжить нажмите любую кнопку");
                Console.ReadKey();
            }
        }
    }

    private static void DrawUserInput()
    {
        try
        {
            Console.WriteLine("Введите имя сотрудника");
            var name = Console.ReadLine() ?? "";

            Console.WriteLine("Введите дату рождения в формате дд/мм/гггг");
            var date = DateTime.Parse(Console.ReadLine() ?? "05/05/2000");

            var appointments = AppointmentAward.Awards.Keys.ToArray();

            Console.WriteLine("Введите номер должности сотрудника");
            for (int i = 0; i < appointments.Length; i++)
            {
                Console.WriteLine($"{i}. {appointments[i]}");
            }

            var num = int.Parse(Console.ReadLine() ?? "0");
            var appointment = appointments[num];

            if (_hourlyEmployees.Contains(appointment))
            {
                Console.WriteLine("Введите почасовую оплату");
            }
            else
            {
                Console.WriteLine("Введите фиксированную плату");
            }

            var rate = double.Parse(Console.ReadLine() ?? "0.0");

            if (_hourlyEmployees.Contains(appointment))
            {
                HourlyEmployee emp = new HourlyEmployee
                {
                    Id = _currId++,
                    Appointment = appointment,
                    DateOfBorn = date,
                    HourlyRate = rate,
                    Name = name
                };

                _organization.Add(emp);
            }
            else
            {
                MonthlyEmployee emp = new MonthlyEmployee
                {
                    Id = _currId++,
                    Appointment = appointment,
                    DateOfBorn = date,
                    MonthlyRate = rate,
                    Name = name
                };
                _organization.Add(emp);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Введены некорректные данные");
        }

        _isInputInProcess = false;
    }

    private static void DrawMenu()
    {
        Console.Clear();
        foreach (var item in MenuItems)
        {
            Console.WriteLine(item);
        }

        SelectMenuItem();
    }

    private static void SelectMenuItem()
    {
        Console.WriteLine("Выберите пункт меню: ");
        var item = Console.ReadLine()?.First() ?? '1';

        if (!char.IsDigit(item))
        {
            Console.WriteLine("Такого пункта меню не существует");
            return;
        }

        var intItem = int.Parse(item.ToString());

        if (intItem is > 8 or < 1)
        {
            Console.WriteLine($"Такого пункта меню не существует: {item}");
            return;
        }

        switch (intItem)
        {
            case 1:
            {
                SortOrganization();
            }
                break;

            case 2:
            {
                PrintAllOrganization();
            }
                break;

            case 3:
            {
                PrintFirstSixNames();
            }
                break;

            case 4:
            {
                PrintLastFourIds();
            }
                break;

            case 5:
            {
                SaveToJson();
            }
                break;

            case 6:
            {
                GetFromJson();
            }
                break;

            case 7:
            {
                AddEmployee();
            }
                break;

            case 8:
            {
                Exit();
            }
                break;
        }
    }

    private static void SortOrganization() => _organization.Sort();

    private static void PrintFirstSixNames()
    {
        for (var i = 0; i < Math.Min(6, _organization.Size); i++)
        {
            Console.WriteLine(_organization[i]);
            Console.WriteLine();
        }
    }

    private static void PrintLastFourIds()
    {
        for (var i = Math.Min(4, _organization.Size - 1); i >= 0; i--)
        {
            Console.WriteLine(_organization[i]);
            Console.WriteLine();
        }
    }

    private static void PrintAllOrganization()
    {
        Console.WriteLine($"Средняя зарплата: {_organization.AverageMonthlySalary}");

        for (var i = 0; i < _organization.Size; i++)
        {
            Console.WriteLine(_organization[i]);
            Console.WriteLine();
        }
    }

    private static void SaveToJson()
    {
        _organization.WriteToJson();
        Console.WriteLine("Организация сохранена");
    }

    private static void GetFromJson()
    {
        Organization? org;

        try
        {
            org = Organization.GetFromJson();
        }
        catch (Exception e)
        {
            Console.WriteLine("Произошла ошибка чтения данных");
            return;
        }

        if (org == null)
        {
            Console.WriteLine("Произошла ошибка чтения данных");
            return;
        }

        _organization = org;
    }

    private static void Exit() => Process.GetCurrentProcess().Kill();

    private static void AddEmployee()
    {
        _isInputInProcess = true;
    }
}