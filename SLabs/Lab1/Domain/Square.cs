namespace SLabs.Lab1.Domain;

public struct Square
{
    public readonly bool IsOpen;
    public readonly int X, Y;

    public Square(int x, int y, bool isOpen = false)
    {
        IsOpen = isOpen;
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}, {Y})";
}