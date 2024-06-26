using System.Reflection;

namespace AreaCalculation
{
    /// <summary>
    /// Discovers marker interface/class implementations.
    /// </summary>
    public static class ImplementationMapper
    {
        /// <summary>
        /// Gets up all implementations info of the marker type in the current <see cref="AppDomain"/>.
        /// </summary>
        /// <param name="marker">A marker type (interface or a base class)</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> map where TKey is a data type for which the implementation is found, and a TValue a <see cref="CalculationAlgorithm"/> info class containing information of the discovered algorithm. If nothing is found, returns an empty dictionary.</returns>
        public static Dictionary<Type, CalculationAlgorithm> Discover(Type marker)
        {
            var implementations = ImplementationMapper.GetImplementations(marker);
            var result = ImplementationMapper.CreateMap(implementations);

            return result;
        }

        /// <summary>
        /// Gets up all implementations of the marker type in the current <see cref="AppDomain"/>.
        /// </summary>
        /// <param name="marker">A marker type (interface or a base class)</param>
        /// <returns>A collection key-value pairs (tuple) where key is an implementing type and value is a target type of the implementation. If nothing is found, returns an empty collection.</returns>
        private static ICollection<(Type, Type)> GetImplementations(Type marker)
        {
            var result =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                from iface in type.GetInterfaces()
                where iface.IsGenericType
                where iface.GetGenericTypeDefinition() == marker
                let implementer = type
                let targetType = iface.GenericTypeArguments.FirstOrDefault()
                select (implementer, targetType);

            return result.ToArray();
        }

        /// <summary>
        /// Creates a map for the specified collection of implementing types and correspondin target types.
        /// </summary>
        /// <param name="implementations">A collection key-value pairs (tuple) where key is an implementing type and value is a target type of the implementation.</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> map where TKey is a target type for which the implementation is found, and a TValue a <see cref="CalculationAlgorithm"/> info class containing information of the discovered algorithm. If nothing is found, returns an empty dictionary.</returns>
        private static Dictionary<Type, CalculationAlgorithm> CreateMap(ICollection<(Type, Type)> implementations)
        {
            var result = new Dictionary<Type, CalculationAlgorithm>();

            foreach (var (implementer, targetType) in implementations)
            {
                var targetMethod = ImplementationMapper.GetMethod(implementer, targetType);
                if (targetMethod is not null)
                {
                    var algorithmInfo = new CalculationAlgorithm(implementer, targetMethod);
                    result.TryAdd(targetType, algorithmInfo);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a method corresponting to the specified target type from the specified implementing type.
        /// </summary>
        /// <param name="implementer">An implementing type of the algorithm.</param>
        /// <param name="targetType">A target type of the method to be discovered.</param>
        /// <returns>A method corresponting to the <paramref name="targetType"/> from the <paramref name="implementer"/> type. Null - if no matched method is found.</returns>
        private static MethodInfo? GetMethod(Type implementer, Type targetType)
        {
            var result =
                (from method in implementer.GetMethods()
                 where method.GetParameters().Count() == 1
                 from parameter in method.GetParameters()
                 where parameter.ParameterType == targetType
                 select method)
                .FirstOrDefault();

            return result;
        }
    }
}
