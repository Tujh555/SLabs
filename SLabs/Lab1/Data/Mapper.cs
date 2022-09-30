using SusuLabs.Lab1.Ang.Domain;

namespace SusuLabs.Lab1.Ang.Data;

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