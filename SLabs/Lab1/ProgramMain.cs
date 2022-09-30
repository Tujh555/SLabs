using SusuLabs.Lab1.Ang.Data;
using SusuLabs.Lab1.Ang.Domain;

namespace SusuLabs.Lab1.Ang;

public static class ProgramMain
{
    public static void Main(string[] args)
    {
        var repo = new Repository();
        var lst = repo.GetMaze();

        Console.WriteLine("Введите через пробел координаты начала ");
        var start = Console.ReadLine()!.Split(" ").Select(int.Parse).ToArray();

        Console.WriteLine("Введите через пробел координаты конца ");
        var end = Console.ReadLine()!.Split(" ").Select(int.Parse).ToArray();
        
        var runner = MazeRunner.Builder
            .SetFinish(end[0], end[1])
            .SetStart(start[0], start[1])
            .SetMaze(lst)
            .Build();

        var result = runner.Run();

        if (result == null)
        {
            Console.WriteLine("Пути нет!");
            return;
        }

        while (result.Size > 0)
        {
            Console.WriteLine(result.Pop());
        }
    }
}