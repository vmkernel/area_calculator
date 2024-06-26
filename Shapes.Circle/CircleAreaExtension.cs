using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Circle;

public static class CircleAreaExtension
{
    public static double GetArea(this Circle circle)
    {
        return Math.Pow(circle.Radius, 2) * Math.PI;
    }
}
