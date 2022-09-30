using SusuLabs.Lab1.Ang.Domain;

namespace SusuLabs.Lab1.Ang.Data;

public class Repository
{
    private const string _path = "C:/_code/Maze.txt";
    private Mapper _mapper = new('0');

    public List<List<Square>> GetMaze()
    {
        using var reader = new StreamReader(_path);
        
        var result = new List<List<Square>>();
        string? line;
        var row = 0;
            
        while ((line = reader.ReadLine()) != null)
        {
            result.Add(_mapper.MapLine(line, row));
            row++;
        }

        return result;
    }
}