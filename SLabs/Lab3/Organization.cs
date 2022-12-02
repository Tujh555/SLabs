using Newtonsoft.Json;
using SLabs.Lab3.Employees;

namespace SLabs.Lab3;

public class Organization
{
    private List<Employee> _list = new();
    private const string FileName = "Organization.json";

    public IEnumerable<Employee> Employees
    {
        get => _list.AsReadOnly();
        set => _list = value.ToList();
    }

    public Employee this[int i] => _list[i];

    [JsonIgnore]
    public int Size => _list.Count;

    [JsonIgnore]
    public double AverageMonthlySalary => _list.Average(emp => emp.Salary);

    public Organization(IEnumerable<Employee> list) => _list.AddRange(list);
    
    public Organization() {}
    
    public void Sort() => _list = _list
        .OrderByDescending(emp => emp.Salary)
        .ThenBy(emp => emp.Name)
        .ToList();

    public void Add(Employee employee) => _list.Add(employee);
    
    public void Delete(int index)
    {
        try
        {
            _list.RemoveAt(index);
        }
        catch (Exception _)
        {
            // ignored
        }
    }

    public void WriteToJson()
    {
        var jsonString = JsonConvert.SerializeObject(
            this,
            Formatting.Indented,
            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
        
        File.WriteAllText(FileName, jsonString);
    }

    public static Organization? GetFromJson()
    {
        var jsonString = File.ReadAllText(FileName);

        return JsonConvert.DeserializeObject<Organization>(
            jsonString,
            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
    }
}