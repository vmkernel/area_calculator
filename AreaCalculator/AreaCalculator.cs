using System.Reflection;
using AreaCalculation;
using AreaCalculation.Shapes;

namespace AreaCalculation;

public static class AreaCalculator
{
    /// <summary>
    /// A types map, where an implementation of <see cref="IAreaCalculator{T}"/> interface (as a value) is mapped to a corresponding class derived from the <see cref="Shape"/> (as a key).
    /// </summary>
    private readonly static Dictionary<Type, Type> RegisteredCalculators = new();

    /// <summary>
    /// A clalculation methods cache, where a calculation method (value) is mapped to a corresponding class derived from the <see cref="Shape"/> (as a key).
    /// </summary>
    private readonly static Dictionary<Type, MethodInfo> MethodCache = new();

    static AreaCalculator()
    {
        RegisteredCalculators = ImplementationMapper.Discover(typeof(IAreaCalculator<>));
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

        if (AreaCalculator.RegisteredCalculators.TryGetValue(shapeType, out var calculator))
        {
            var calculationMethod = GetCalculationMethod(shapeType);
            var calculatorInstance = Activator.CreateInstance(calculator);

            result = calculationMethod?.Invoke(calculatorInstance, new[] { shape });
        }

        return result is not null
            ? (double)result
            : throw new NotSupportedException($"An area calculation method for shape '{shapeType}' is not exists.");
    }

    /// <summary>
    /// Gets an area calculation method corresponting to the <paramref name="shapeType"/> and caches it in <see cref="MethodCache"/> if absent.
    /// </summary>
    /// <param name="shapeType">A derived type of <see cref="Shape"/>.</param>
    /// <returns>An area calculation method or NULL - if a corresponding method is not found neither in the cache nor in the <see cref="RegisteredCalculators"/> implementations map.</returns>
    private static MethodInfo? GetCalculationMethod(Type shapeType)
    {
        if (!AreaCalculator.MethodCache.TryGetValue(shapeType, out var result) &&
            AreaCalculator.RegisteredCalculators.TryGetValue(shapeType, out var calculator))
        {
            result =
                (from method in calculator.GetMethods()
                 where method.GetParameters().Count() == 1
                 from parameter in method.GetParameters()
                 where parameter.ParameterType == shapeType
                 select method)
                .FirstOrDefault();

            if (result is not null)
            {
                MethodCache.TryAdd(shapeType, result);
            }
        }
        
        return result;
    }
}