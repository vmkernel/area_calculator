namespace AreaCalculation.Shapes;

public abstract class Shape
{
    public abstract double Area { get; protected set; }

    protected static int NormalizeLength(int length)
    {
        return length < 0 ? Math.Abs(length) : length;
    }
}
