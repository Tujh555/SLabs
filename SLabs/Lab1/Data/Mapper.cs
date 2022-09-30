using SLabs.Lab1.Domain;

namespace SLabs.Lab1.Data;

public class Mapper
{
    private readonly char _openSquare;

    public Mapper(char openSquare)
    {
        _openSquare = openSquare;
    }

    public List<Square> MapLine(string line, int row)
    {
        var column = 0;
        
        return line
            .Replace(", ", "")
            .Select(c => new Square(row, column++, c == _openSquare))
            .ToList();
    }
}