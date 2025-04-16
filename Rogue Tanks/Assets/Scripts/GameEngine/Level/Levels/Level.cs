using System;
using UnityEngine;

public class Level : ILevel
{
    public int[,] LevelMatrix { get; private set; }

    public PopList<int> Tanks { get; private set; }

    public Vector3[] SpawnLocations { get; private set; }

    public Vector3 PlayerTankPosition { get; private set; }

    private LevelDimensions levelDimensions = LevelDimensions.CreateStandard();
    public LevelDimensions LevelDimensions { get => levelDimensions; }

    public int LevelNumber => 0;

    public Level()
    {
        LevelMatrix = new int[,] { };
        Tanks = PopList<int>.CreateEmpty();
        SpawnLocations = new Vector3[0];
        PlayerTankPosition = new Vector3(0, 0, 0);
    }

    public ILevel LoadNextLevel() => new Level_1();

    public ILevel IsEndOfGame(Action onEndOfGame) => this;

    public ILevel IsNotEndOfGame(Action onNotEndOfGame)
    {
        onNotEndOfGame();
        return this;
    }
}
