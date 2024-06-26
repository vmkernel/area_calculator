﻿namespace Shapes;

public abstract class Shape
{
    protected static int NormalizeLength(int length)
    {
        return length < 0 ? Math.Abs(length) : length;
    }
}
