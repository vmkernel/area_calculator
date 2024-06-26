namespace Shapes.Circle;

public class Circle : Shape
{
    private readonly double area;

    public override double Area => 
        this.area;

    public int Radius { get; init; }

    public Circle(int radius)
    {
        this.Radius = Shape.NormalizeLength(radius);
        this.area = this.GetArea();
    }
}
