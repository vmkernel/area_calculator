using AreaCalculation.Shapes;

namespace AreaCalculation;

/// <summary>
/// A marker inteface for implementation of area calculation algorithm.
/// </summary>
/// <typeparam name="T">A class derived from <see cref="Shape"/> describing a 2D-shape primitive.</typeparam>
public interface IAreaCalculator<T>
    where T : Shape
{
    /// <summary>
    /// Gets an area of the shape.
    /// </summary>
    /// <param name="shape">A shape which area should be calculated.</param>
    /// <returns>Area of the <paramref name="shape"/>.</returns>
    double GetArea(T shape);
}
