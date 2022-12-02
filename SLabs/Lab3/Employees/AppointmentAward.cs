namespace SLabs.Lab3.Employees;

public static class AppointmentAward
{
    public static readonly Dictionary<string, double> Awards = new()
    {
        { "Менеджер", 12_500.3 },
        { "Учитель", 50_000 },
        { "Редактор", 10_000.5 },
        { "Режиссер", 8_000.9 },
        { "Монтажер", 30_000 },
    };
    
}