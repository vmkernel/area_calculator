namespace AreaCalculation.Shapes.Triangle;

public class Triangle : Shape
{
    public override double Area { get; protected set; }

    public int SideA { get; init; }

    public int SideB { get; init; }

    public int SideC { get; init; }

    public bool IsRight { get; private set; }

    public Triangle(int a, int b, int c)
    {
        this.SideA = Shape.NormalizeLength(a);
        this.SideB = Shape.NormalizeLength(b);
        this.SideC = Shape.NormalizeLength(c);

        this.IsRight = Triangle.IsRightTriangle(this);
        this.Area = this.GetArea();
    }

    private static bool IsRightTriangle(Triangle triangle)
    {
        var sides = new List<int>
        {
            triangle.SideA,
            triangle.SideB,
            triangle.SideC,
        };

        sides.Sort();
        sides.Reverse();

        return Math.Pow(sides[0], 2) == (Math.Pow(sides[1], 2) + Math.Pow(sides[2], 2));
    }
}
