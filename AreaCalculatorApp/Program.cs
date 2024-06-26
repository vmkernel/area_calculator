using AreaCalculation;
using AreaCalculation.Shapes.Circle;
using AreaCalculation.Shapes.Triangle;

namespace AreaCalculatorApp;

internal static class Program
{
    static void Main()
    {
        var circleArea = AreaCalculator.GetArea(new Circle(5));
        var triangleArea = AreaCalculator.GetArea(new Triangle(2, 4, 3));
        var rightTriangleArea = AreaCalculator.GetArea(new Triangle(8, 6, 10));

        Console.WriteLine($"Area of the circle is: {circleArea}.");
        Console.WriteLine($"Area of the triangle 1 is: {triangleArea}.");
        Console.WriteLine($"Area of the triangle 2 is: {rightTriangleArea}.");

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
