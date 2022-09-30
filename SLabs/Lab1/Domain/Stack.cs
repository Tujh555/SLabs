namespace SusuLabs.Lab1.Ang.Domain;

public class Stack<T>
{
    private int _capacity;
    private T[] _arr;
    public int Size { get; private set; }

    public Stack(int capacity = 10)
    {
        _capacity = capacity;
        _arr = new T[_capacity];
    }
    
    public void Put(T item)
    {
        if (Size + 1 > _capacity)
        {
            _capacity *= 2;
            var arr = new T[_capacity];
            Array.Copy(_arr, arr, _arr.Length);
            _arr = arr;
        }

        _arr[Size++] = item;
    }

    public T Pop() => _arr[Size -= 1];
}