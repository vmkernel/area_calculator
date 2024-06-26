namespace Shapes;

public abstract class Shape
{
    public abstract double Area { get; }

    protected static int NormalizeLength(int length)
    {
        return length < 0 ? Math.Abs(length) : length;
    }
}
