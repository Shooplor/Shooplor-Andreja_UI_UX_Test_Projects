using System;
using UnityEngine;

public static class VectorFactory
{
    public static Vector3 Random(Tuple<int,int> x, Tuple<int,int> y)
    {
        return new Vector3(Probability.GenerateRandomNumberBetween(x.Item1, x.Item2), Probability.GenerateRandomNumberBetween(y.Item1, y.Item2));
    }

    public static Vector3 CreateFromTuple(Tuple<int, int> tuple) => new Vector3(tuple.Item1, tuple.Item2);
}
