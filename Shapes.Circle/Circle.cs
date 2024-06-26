using AreaCalculation.Shapes;

namespace AreaCalculation.Shapes.Circle;

public class Circle : Shape
{
    public int Radius { get; init; }

    public Circle(int radius)
    {
        this.Radius = Shape.NormalizeLength(radius);
    }
}
