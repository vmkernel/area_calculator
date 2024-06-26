using System.Reflection;
using AreaCalculation;
using AreaCalculation.Shapes;

namespace AreaCalculation;

public static class AreaCalculator
{
    /// <summary>
    /// A types map, where an implementation of <see cref="IAreaCalculator{T}"/> interface (as a value) is mapped to a corresponding descriptor class <see cref="CalculationAlgorithm"/>.
    /// </summary>
    private readonly static Dictionary<Type, CalculationAlgorithm> RegisteredAlgorithms = new();

    static AreaCalculator()
    {
        AreaCalculator.RegisteredAlgorithms = ImplementationMapper.Discover(typeof(IAreaCalculator<>));
    }

    /// <summary>
    /// Calculates an area of a <paramref name="shape"/> using a corresponding calculator implementation of <see cref="IAreaCalculator{T}"/>.
    /// </summary>
    /// <param name="shape">A shape which area should be calculated.</param>
    /// <returns>Area of the <paramref name="shape"/>.</returns>
    /// <exception cref="NotImplementedException">When a specified <paramref name="shape"/> has no implementation of <see cref="IAreaCalculator{T}"/>.</exception>
    public static double GetArea(Shape shape)
    {
        object? result = null;
        var shapeType = shape.GetType();

        if (AreaCalculator.RegisteredAlgorithms.TryGetValue(shapeType, out var algorithmInfo) && algorithmInfo is not null)
        {
            var instance = Activator.CreateInstance(algorithmInfo.ImplementingType);
            result = algorithmInfo.TargetMethod.Invoke(instance, new[] { shape });
        }

        return result is not null
            ? (double)result
            : throw new NotSupportedException($"An area calculation method for shape '{shapeType}' is not exists.");
    }
}