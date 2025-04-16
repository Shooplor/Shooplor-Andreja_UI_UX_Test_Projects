using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILevel
{
    public int LevelNumber { get; }
    LevelDimensions LevelDimensions { get; }
    int[,] LevelMatrix { get; }
    PopList<int> Tanks { get; }
    Vector3[] SpawnLocations { get; }
    Vector3 PlayerTankPosition { get; }
    ILevel LoadNextLevel();
    ILevel IsEndOfGame(Action onEndOfGame);
    ILevel IsNotEndOfGame(Action onNotEndOfGame);
}
