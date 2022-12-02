using System.Text.Json.Serialization;

namespace SLabs.Lab3.Employees;

public class MonthlyEmployee : Employee
{
    public double MonthlyRate { get; set; }
    
    public override double Salary => MonthlyRate;

    public override string ToString()
    {
        return $"{Name}\n" +
               $"{DateOfBorn}\n" +
               $"{Appointment}\n" +
               $"Зарплата в месяц: {MonthlyRate}\n" +
               $"Премия: {Award}";
    }
}