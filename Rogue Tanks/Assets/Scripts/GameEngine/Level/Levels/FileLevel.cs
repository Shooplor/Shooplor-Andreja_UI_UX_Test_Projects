using System;
using System.Collections.Generic;
using UnityEngine;

public class FileLevel : ILevel
{
    public FileLevel(int levelNumber)
    {
        LevelNumber = levelNumber;
        TextAsset mytxtData = (TextAsset)Resources.Load($"Stage_{levelNumber}");
        if (mytxtData != null)
        {
            var lines = mytxtData.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var spawnLocationsText = lines[0].Split(':')[1].Split(';');
            List<Vector3> tempSpawnLocations = new List<Vector3>();
            foreach(var spawnLocationText in spawnLocationsText)
            {
                var l = spawnLocationText.Split(',');
                tempSpawnLocations.Add(LevelDimensions.ToLevelPositionVector(Convert.ToInt32(l[0]), Convert.ToInt32(l[1])));
            }
            SpawnLocations = tempSpawnLocations.ToArray();
            var enemyTanks = lines[1].Split(':')[1].Split(',');
            if (enemyTanks.Length > 1)
            {
                List<int> tempEnemyTanks = new List<int>();
                foreach (var et in enemyTanks)
                    tempEnemyTanks.Add(Convert.ToInt32(et));
                Tanks = PopList<int>.Create(tempEnemyTanks.ToArray());
            }
            else
                Tanks = PopList<int>.CreateEmpty();

            List<int[]> tempMap = new List<int[]>();
            for(int i = 2; i < lines.Length; i++)
            {
                var elements = lines[i].Split(',');
                List<int> elList = new List<int>();
                foreach (var el in elements)
                    elList.Add(Convert.ToInt32(el.Trim()));
                tempMap.Add(elList.ToArray());
            }
            LevelMatrix = tempMap.ToNormalArray();
            var levelPosition = LevelMatrix.GetPositionOf(5);
            PlayerTankPosition = LevelDimensions.ToLevelPositionVector(levelPosition.Item1, levelPosition.Item2).AdjustForTank();
        }
    }
    public int LevelNumber { get; private set; } = 0;

    public LevelDimensions LevelDimensions { get; private set; } = LevelDimensions.CreateStandard();

    public int[,] LevelMatrix { get; private set; } = new int[0, 0];

    public PopList<int> Tanks { get; private set; } = PopList<int>.CreateEmpty();

    public Vector3[] SpawnLocations { get; private set; } = new Vector3[0];

    public Vector3 PlayerTankPosition { get; private set; }

    public ILevel IsEndOfGame(Action onEndOfGame)
    {
        TextAsset mytxtData = (TextAsset)Resources.Load($"Stage_{LevelNumber}");
        if (mytxtData==null)
            onEndOfGame();
        return this;
    }

    public ILevel IsNotEndOfGame(Action onNotEndOfGame)
    {
        TextAsset mytxtData = (TextAsset)Resources.Load($"Stage_{LevelNumber}");
        if (mytxtData != null)
            onNotEndOfGame();
        return this;
    }

    public ILevel LoadNextLevel()
    {
        return new FileLevel(LevelNumber + 1);
    }
}
