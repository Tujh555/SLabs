using System.Text.RegularExpressions;

namespace SLabs.Lab4;

public static class Program
{
    private static readonly CsvWriter CsvWriter = new("references.csv");
    private static readonly List<Reference> References = new();

    public static void Main(string[] args)
    {
        Console.WriteLine("Введите адрес сайта");
        var url = Console.ReadLine() ?? "";

        while (!Regex.IsMatch(url, "(http|https)://.*/"))
        {
            Console.WriteLine("Введенный адрес некорректен. Повторите ввод.");
            url = Console.ReadLine() ?? "";
        }

        Console.WriteLine("Введите количество страниц");
        var str = Console.ReadLine() ?? "10";
        int pageCount;

        while (!int.TryParse(str, out pageCount))
        {
            Console.WriteLine("Введите число");
            str = Console.ReadLine();
        }

        var scanner = new Scanner();
        scanner.OnReferenceFound += WriteReference;
        scanner.OnReferenceFound += SaveReference;
        
        scanner.Process(url, pageCount);
        
        CsvWriter.Write(References, ";");
    }

    private static void WriteReference(Reference reference)
    {
        Console.WriteLine(reference);
    }

    private static void SaveReference(Reference reference)
    {
        References.Add(reference);
    }
}