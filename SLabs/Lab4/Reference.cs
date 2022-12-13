namespace SLabs.Lab4;

public class Reference
{
    public string Home { get; set; }
    public string Ref { get; set; }
    public string Name { get; set; }
    public int Lvl { get; set; }

    public override string ToString()
    {
        return $"Page: {Home}; Reference: {Ref}; Title: {Name}; Level: {Lvl}";
    }
}