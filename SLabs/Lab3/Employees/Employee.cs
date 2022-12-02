using Newtonsoft.Json;

namespace SLabs.Lab3.Employees;

public abstract class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBorn { get; set; }
    public string Appointment { get; set; }
    
    [JsonIgnore]
    public abstract double Salary { get; }
    
    [JsonIgnore]
    public double Award => AppointmentAward.Awards.ContainsKey(Appointment)
        ? AppointmentAward.Awards[Appointment]
        : 0.0;
}