using System;
using System.Collections.Generic;
using UnityEngine;

public class Level_2 : ILevel
{
    public int[,] LevelMatrix { get; private set; }

    public PopList<int> Tanks { get; private set; }

    public Vector3[] SpawnLocations { get; private set; }

    public Vector3 PlayerTankPosition { get; private set; }

    private LevelDimensions levelDimensions = LevelDimensions.CreateStandard();
    public LevelDimensions LevelDimensions { get => levelDimensions; }

    public int LevelNumber => 2;

    public ILevel LoadNextLevel() => this;

    public ILevel IsEndOfGame(Action onEndOfGame)
    {
        onEndOfGame();
        return this;
    }

    public ILevel IsNotEndOfGame(Action onNotEndOfGame) => this;

    public Level_2()
    {
        LevelMatrix = new int[,] { };
        Tanks = PopList<int>.CreateEmpty();
        SpawnLocations = new Vector3[0];
        PlayerTankPosition = new Vector3(0, 0, 0);
    }
}
