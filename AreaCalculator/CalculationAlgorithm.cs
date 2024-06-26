using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AreaCalculation
{
    /// <summary>
    /// A description for an area calculating algorithm.
    /// </summary>
    public class CalculationAlgorithm
    {
        /// <summary>
        /// A type implementing the algorithm.
        /// </summary>
        public Type ImplementingType { get; init; }

        /// <summary>
        /// A target method permorming the calculation.
        /// </summary>
        public MethodInfo TargetMethod { get; init; }

        public CalculationAlgorithm(Type implementingType, MethodInfo targetMethod)
        {
            this.ImplementingType = implementingType;
            this.TargetMethod = targetMethod;
        }
    }
}
