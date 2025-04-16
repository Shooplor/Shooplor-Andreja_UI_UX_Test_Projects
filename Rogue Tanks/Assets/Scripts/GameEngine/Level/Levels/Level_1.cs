using UnityEngine;

public class Level_1 : ILevel
{
    public int[,] LevelMatrix { get; private set; }
    public PopList<int> Tanks { get; private set; }
    public Vector3[] SpawnLocations { get; private set; }

    public Vector3 PlayerTankPosition => levelDimensions.ToLevelPositionVector(8, 25).AdjustForTank();

    private LevelDimensions levelDimensions = LevelDimensions.CreateStandard();
    public LevelDimensions LevelDimensions { get => levelDimensions; }

    public int LevelNumber => 1;

    public Level_1()
    {
        LevelMatrix = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,4,4,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,4,4,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,1,0,0},
            {0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0},
            {1,1,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,1,1},
            {4,4,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,4,4},
            {0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,1,1,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,1,0,0},
            {0,0,1,1,0,0,1,1,0,0,0,0,0,0,0,0,0,0,1,1,0,0,1,1,0,0},
            {0,0,0,0,0,0,0,0,5,0,0,0,10,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
        Tanks = PopList<int>.Create(6, 6, 6, 6, 6, 7, 6, 6, 6, 6, 6, 6, 6, 7, 6, 6, 6, 6, 6, 6); 
        SpawnLocations = new Vector3[] { levelDimensions.ToLevelPositionVector(0,0), levelDimensions.ToLevelPositionVector(12,0), levelDimensions.ToLevelPositionVector(25, 0) };
    }

    public ILevel LoadNextLevel() => new Level_2();

    public ILevel IsEndOfGame(System.Action onEndOfGame) => this;

    public ILevel IsNotEndOfGame(System.Action onNotEndOfGame)
    {
        onNotEndOfGame();
        return this;
    }
}
