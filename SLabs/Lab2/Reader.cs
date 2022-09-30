namespace SLabs.Lab2;

public static class Reader
{
    public static (double real, double imaginary) ReadComplexData(string line)
    {
        var arr = line.Split(" ").Select(s => s.Trim()).ToArray();

        return arr[0].Contains('i') 
            ? (double.Parse(arr[1].Replace("i", "")), double.Parse(arr[0])) 
            : (double.Parse(arr[0].Replace("i", "")), double.Parse(arr[1]));
    }

    public static char ReadArithmeticOperation(string line) => line.Trim().First();
}