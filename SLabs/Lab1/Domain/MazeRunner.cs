namespace SusuLabs.Lab1.Ang.Domain;

public class MazeRunner
{
    private readonly List<List<Square>> _maze;
    private readonly (int x, int y) _start;
    private readonly (int x, int y) _finish;
    public static RunnerBuilder Builder => new();

    private MazeRunner(List<List<Square>> maze, (int x, int y) start, (int x, int y) finish)
    {
        _maze = maze;
        _start = start;
        _finish = finish;
    }

    public Stack<Square>? Run()
    {
        var matrix = GetEmptyMatrix();
        var step = 0;

        while (matrix[_finish.x][_finish.y] == 0)
        {
            MakeStep(matrix, ++step);
        }

        var result = new Stack<Square>((matrix.Length - 1) * (matrix[0].Length - 1) );
        (int i, int j) = _finish;
        var k = matrix[i][j];
        result.Put(_maze[i][j]);

        while (k > 1)
        {
            if (i > 0 && matrix[i - 1][j] == k - 1)
            {
                i--;
            } else if (j > 0 && matrix[i][j - 1] == k - 1)
            {
                j--;
            } else if (i < matrix.Length - 1 && matrix[i + 1][j] == k - 1)
            {
                i++;
            } else if (j < matrix[i].Length - 1 && matrix[i][j + 1] == k - 1)
            {
                j++;
            }
            else
            {
                return null;
            }
            
            result.Put(_maze[i][j]);
            k--;
        }

        return result;
    }
    

    private void MakeStep(int[][] matrix, int step)
    {
        for (var i = 0; i < matrix.Length; i++)
        {
            for (var j = 0; j < matrix[i].Length; j++)
            {
                if (matrix[i][j] == step)
                {
                    if (i > 0 && matrix[i - 1][j] == 0 && _maze[i - 1][j].IsOpen)
                        matrix[i - 1][j] = step + 1;

                    if (j > 0 && matrix[i][j - 1] == 0 && _maze[i][j - 1].IsOpen)
                        matrix[i][j - 1] = step + 1;

                    if (i < matrix.Length - 1 && matrix[i + 1][j] == 0 && _maze[i + 1][j].IsOpen)
                        matrix[i + 1][j] = step + 1;

                    if (j < matrix[i].Length - 1 && matrix[i][j + 1] == 0 && _maze[i][j + 1].IsOpen)
                        matrix[i][j + 1] = step + 1;
                }
            }
        }
    }

    private int[][] GetEmptyMatrix() => _maze.Select(lst =>
    {
        return lst.Select(square => 
            square.X == _start.x && square.Y == _start.y 
                ? 1 
                : 0
        ).ToArray();
        
    }).ToArray();
    
    public class RunnerBuilder
    {
        private List<List<Square>>? _maze;
        private (int, int)? _start;
        private (int, int)? _finish;

        public RunnerBuilder SetMaze(List<List<Square>> maze)
        {
            _maze = maze;
            return this;
        }

        public RunnerBuilder SetStart(int x, int y)
        {
            _start = (x, y);
            return this;
        }

        public RunnerBuilder SetFinish(int x, int y)
        {
            _finish = (x, y);
            return this;
        }

        public MazeRunner Build()
        {
            if (_maze == null)
            {
                throw new ArgumentNullException( $"Maze must not be null");
            }

            return new MazeRunner(
                _maze,
                _start ?? (0, 0),
                _finish ?? (_maze[0].Count, _maze.Count));
        }
    }
}