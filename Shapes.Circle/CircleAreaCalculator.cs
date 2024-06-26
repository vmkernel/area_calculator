namespace AreaCalculation.Shapes.Circle;

public class CircleAreaCalculator : IAreaCalculator<Circle>
{
    public double GetArea(Circle shape)
    {
        return Math.Pow(shape.Radius, 2) * Math.PI;
    }
}
