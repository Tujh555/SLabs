namespace SLabs.Lab2;

public static class Reader
{
    public static (double, double) GetComplexData(string line)
    {
        var arr = line.Split(" ").Select(s => s.Trim()).ToArray();
        Console.WriteLine($"{arr[0]} {arr[1]}");

        return arr[0].Contains('i') 
            ? (double.Parse(arr[1]), double.Parse(arr[0].Replace("i", ""))) 
            : (double.Parse(arr[0]), double.Parse(arr[1].Replace("i", "")));
    }
}