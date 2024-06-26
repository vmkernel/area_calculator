namespace AreaCalculation.Shapes.Triangle;

public static class TriangleAreaExtension
{
    public static double GetArea(this Triangle triangle)
    {
        var height = GetHeight(triangle);
        var result = (triangle.SideA * height) / 2;

        return result;

    }
    private static double GetHeight(Triangle triangle)
    {
        var p = GetHalfPerimeter(triangle);

        var sqrtParam = p * ((p - triangle.SideA) * (p - triangle.SideB) * (p - triangle.SideC));
        var result = 2 / triangle.SideA * Math.Sqrt(sqrtParam);

        return result;
    }

    private static double GetHalfPerimeter(Triangle triangle)
    {
        return (triangle.SideA + triangle.SideB + triangle.SideC) / (double)2;
    }
}
