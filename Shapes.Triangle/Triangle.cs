using AreaCalculation.Shapes;

namespace AreaCalculation.Shapes.Triangle;

public class Triangle : Shape
{
    public int SideA { get; init; }

    public int SideB { get; init; }

    public int SideC { get; init; }

    public Triangle(int a, int b, int c)
    {
        this.SideA = Shape.NormalizeLength(a);
        this.SideB = Shape.NormalizeLength(b);
        this.SideC = Shape.NormalizeLength(c);
    }
}
