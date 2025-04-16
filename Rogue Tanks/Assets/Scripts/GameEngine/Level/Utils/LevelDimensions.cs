using UnityEngine;

public class LevelDimensions
{
    public int StartX { get; private set; }
    public int EndX { get; private set; }
    public int StartY { get; private set; }
    public int EndY { get; private set; }

    private int minsize;
    private int maxSize;

    private LevelDimensions(int startX, int endX, int startY, int endY, int min, int max)
    {
        StartX = startX;
        EndX = endX;
        StartY = startY;
        EndY = endY;
        minsize = min;
        maxSize = max;
    }

    internal int ToX(Vector3 pos) => ((int)pos.x).Normalize(minsize,maxSize, StartX,EndX);
    internal int ToY(Vector3 pos) => ((int)pos.y).Normalize(minsize, maxSize, StartY, EndY);

    public Vector3 ToLevelPositionVector(int x, int y) => 
        new Vector3(x.Normalize(StartX, EndX, minsize, maxSize), y.Normalize(StartY, EndY, minsize, maxSize), 0);

    public static LevelDimensions CreateStandard() => new LevelDimensions(-25, 25, 25, -25, 0, 25);

    internal Vector3 RandomUpgradePosition() => VectorFactory.Random(new System.Tuple<int, int>(StartX + 1, EndX - 1), new System.Tuple<int, int>(StartY - 1, EndY + 1));
}
