using System.Text.Json.Serialization;

namespace SLabs.Lab3.Employees;

public class HourlyEmployee : Employee
{
    public double HourlyRate { get; set; }
    
    public override double Salary => 20.8 * 8 * HourlyRate;
    
    public override string ToString()
    {
        return $"{Name}\n" +
               $"{DateOfBorn}\n" +
               $"{Appointment}\n" +
               $"Почасовая ставка: {HourlyRate}\n" +
               $"Премия: {Award}";
    }
}