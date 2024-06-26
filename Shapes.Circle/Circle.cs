namespace AreaCalculation.Shapes.Circle;

public class Circle : Shape
{
    public override double Area { get; protected set; }

    public int Radius { get; init; }

    public Circle(int radius)
    {
        this.Radius = Shape.NormalizeLength(radius);
        this.Area = this.GetArea();
    }
}
