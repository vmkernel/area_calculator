using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AreaCalculation
{
    /// <summary>
    /// Discovers marker interface/class implementations.
    /// </summary>
    public static class ImplementationMapper
    {
        /// <summary>
        /// Looks up all implementations of the <paramref name="marker"/> type (interface or a base class) in the current <see cref="AppDomain"/>
        /// </summary>
        /// <param name="marker">A marker type</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> map where TKey is a data type for which the implementation is found, and a TValue is the implementing type. If no types are found, returns an empty dictionary.</returns>
        public static Dictionary<Type, Type> Discover(Type marker)
        {
            var result = new Dictionary<Type, Type>();

            var implementations =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                from iface in type.GetInterfaces()
                where iface.IsGenericType
                where iface.GetGenericTypeDefinition() == marker
                let parameter = iface.GenericTypeArguments.FirstOrDefault()
                select (type, parameter);

            foreach (var (calculator, type) in implementations)
            {
                if (calculator is not null && type is not null)
                {
                    result.TryAdd(type, calculator);
                }
            }

            return result;
        }
    }
}
