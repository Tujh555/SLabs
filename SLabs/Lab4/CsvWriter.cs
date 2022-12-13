using System.Text;

namespace SLabs.Lab4;

public class CsvWriter
{
    private readonly string _filePath;

    public CsvWriter(string filePath)
    {
        _filePath = filePath;
    }

    public void Write(IEnumerable<Reference> references, string divider)
    {
        var data = "";

        foreach (var reference in references)
        {
            data += ToCsv(reference, divider) + '\n';
        }
        
        File.WriteAllText(_filePath, data, Encoding.UTF8);
    }

    private static string ToCsv(Reference reference, string divider)
    {
        var res = reference.Home + divider + reference.Name + divider + reference.Ref + divider + reference.Lvl;
        return res;
    }
}