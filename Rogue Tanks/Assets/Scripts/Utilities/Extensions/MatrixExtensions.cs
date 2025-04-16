using System;

public static class MatrixExtensions
{
    public static void ForEach<T>(this T[,] matrix, Action<int,int,T> onElement)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
            for (int j = 0; j < matrix.GetLength(1); j++)
                onElement(j, i, matrix[i, j]);
    }

    public static Tuple<int, int> GetPositionOf(this int[,] matrix, int element)
    {
        var result = Tuple.Create(0, 0);
        bool isDone = false;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (element == matrix[i, j])
                {
                    result = Tuple.Create(j, i);
                    isDone = true;
                    break;
                }
            }
            if (isDone) break;
        }

        return result;
    }
}
