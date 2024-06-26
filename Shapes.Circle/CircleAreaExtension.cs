namespace AreaCalculation.Shapes.Circle;

public static class CircleAreaExtension
{
    public static double GetArea(this Circle circle)
    {
        return Math.Pow(circle.Radius, 2) * Math.PI;
    }
}
