using AreaCalculation;
using AreaCalculation.Shapes;
using NUnit.Framework.Internal;
using System.Reflection;
using System.Runtime.Loader;

namespace AreaCalculator.Tests
{
    public class Consistency
    {
        private const string ClientAssemblyName = "AreaCalculatorApp";

        private readonly Type ShapesBaseClass = typeof(Shape);

        private readonly Type MarkerInterface = typeof(IAreaCalculator<>);

        [OneTimeSetUp]
        public void Setup()
        {          
            var clientAssembly = Assembly.Load(ClientAssemblyName);
            var dependencies = clientAssembly.GetReferencedAssemblies();
            foreach (var assembly in dependencies)
            {
                Assembly.Load(assembly);
            }
        }

        [Test]
        public void All_Calculators_Implemented()
        {
            var shapes = GetDerivedClasses(ShapesBaseClass);
            var calculators = GetGenericInterfaceImpl(MarkerInterface);

            foreach (var shape in shapes)
            {
                var query =
                    from calc in calculators
                    let shapeType = calc.Item2
                    let calcImpl = calc.Item1
                    where shapeType == shape
                    select calcImpl;

                if (!query.Any())
                {
                    Assert.Fail($"Area calculation algorithm implementation for shape '{shape.Name}' is not found.");
                }
            }

            Assert.Pass();
        }

        private static ICollection<Type> GetDerivedClasses(Type baseType)
        {
            var query =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.BaseType == baseType
                select type;

            return query.ToArray() ?? Array.Empty<Type>();
        }

        private static ICollection<(Type, Type)> GetGenericInterfaceImpl(Type genericInterface)
        {
            var query =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                from iface in type.GetInterfaces()
                where iface.IsGenericType
                where iface.GetGenericTypeDefinition() == genericInterface
                let parameter = iface.GenericTypeArguments.FirstOrDefault()
                select (type, parameter);

            return query.ToArray() ?? Array.Empty<(Type, Type)>();
        }
    }
}