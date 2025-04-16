using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectBuilder
{
    private readonly GameEngine gameEngine;
    private ILevel level;
    private Dictionary<int, Action<Vector3>> mapper;

    public GameObjectBuilder(GameEngine gameEngine)
    {
        this.gameEngine = gameEngine;
        mapper = new Dictionary<int, Action<Vector3>>
        {
            { 0, pos => { } },
            { 1, pos => gameEngine.GameFactory.CreateBrick(pos, level.LevelDimensions.ToY(pos), level.LevelDimensions.ToX(pos)) },
            { 2, pos => gameEngine.GameFactory.CreateForest(pos, level.LevelDimensions.ToY(pos), level.LevelDimensions.ToX(pos)) },
            { 3, pos => gameEngine.GameFactory.CreateWater(pos, level.LevelDimensions.ToY(pos), level.LevelDimensions.ToX(pos)) },
            { 4, pos => gameEngine.GameFactory.CreateStone(pos, level.LevelDimensions.ToY(pos), level.LevelDimensions.ToX(pos)) },
            { 5, pos => RespawnOrCreatePlayerTank(pos) },
            { 6, pos => gameEngine.GameFactory.CreateTank(pos, tank => tank.CreateEnemyTank1("2", gameEngine)) },
            { 7, pos => gameEngine.GameFactory.CreateTank(pos, tank => tank.CreateEnemyTank2("2", gameEngine)) },
            { 8, pos => gameEngine.GameFactory.CreateTank(pos, tank => tank.CreateEnemyTank3("2", gameEngine)) },
            { 9, pos => gameEngine.GameFactory.CreateTank(pos, tank => tank.CreateEnemyTank4("2", gameEngine)) },
            { 10, pos => gameEngine.GameFactory.CreateTankBase(pos, _ => { }) },
            { 11, pos => gameEngine.GameFactory.CreateIce(pos, level.LevelDimensions.ToY(pos), level.LevelDimensions.ToX(pos)) },
            { 12, pos => gameEngine.GameFactory.CreateLava(pos, level.LevelDimensions.ToY(pos), level.LevelDimensions.ToX(pos)) },
            { 13, pos => gameEngine.GameFactory.CreateMud(pos, level.LevelDimensions.ToY(pos), level.LevelDimensions.ToX(pos)) }
        };
    }

    private void RespawnOrCreatePlayerTank(Vector3 position)
    {
        var playerTank = gameEngine.ActiveGameObjectsController.ActiveTanks.GetPlayerTanks().FirstOrDefault();
        if (playerTank == null)
        {
            gameEngine.GameFactory.CreateTank(position, tank => tank.CreatePlayerTank("1", gameEngine));
        }
        else
        {
            playerTank.SpawnAt(position.AdjustForTank(),1, sc=>sc.FaceUp());
        }
    }

    private void ClearLevel()
    {
        gameEngine.GameTiles.Reset();
        gameEngine.ActiveGameObjectsController.ActiveTanks.GetPlayerTanks().ForEach(pt => pt.gameObject.Deactivate());
    }

    public void BuildLevel(ILevel level)
    {
        this.level = level;
        ClearLevel();
        CreateScoreboard(level);
        CreateLevelFromMatrix(level);
        CreateInitialTanks(level);
        CreateTanksAtRandomTimeIntervals(level);
    }

    private void CreateScoreboard(ILevel level)=>
        gameEngine.ScoreboardController.CreateScoreboardTanks(level.Tanks.Count);

    private void CreateTanksAtRandomTimeIntervals(ILevel level) =>
        gameEngine.GameUtilities.ExecuteInTimeIntervalBetween(3.0f, 6.0f, 
            CreateEnemyTanks,
            level.Tanks.HasNoMoreElements);

    private void CreateEnemyTanks() =>
        GenerateSpawnLocations()
            .ForEach(spawnLocation => level.Tanks.Pop(tankType => CreateGameObject(tankType, spawnLocation)));

    private IEnumerable<Vector3> GenerateSpawnLocations() => level.SpawnLocations.GetRandomItems().Where(ThereIsNoTankAtSpawnPosition);
    
    private bool ThereIsNoTankAtSpawnPosition(Vector3 position) => gameEngine.ActiveGameObjectsController.ActiveTanks.All(tank => AreAtAppropriateDistance(tank, position));

    private bool AreAtAppropriateDistance(Tank tank, Vector3 spawnLocation) => Vector3.Distance(tank.transform.position, spawnLocation).IsGreaterThanOrEqualTo(6);
    private void CreateInitialTanks(ILevel level) =>
        gameEngine.GameUtilities.WaitFor(5, () =>
        level.Tanks.Pop(3).For((index, item) => CreateGameObject(item, level.SpawnLocations[index])));

    private void CreateLevelFromMatrix(ILevel level) =>
        level.LevelMatrix.ForEach((x, y, element) => CreateGameObject(element, level.LevelDimensions.ToLevelPositionVector(x, y)));

    private void CreateGameObject(int element, Vector3 position)
    {
        element.If(IsEnemyTank, gameEngine.ScoreboardController.RemoveScoreboardTank);
        mapper[element].Invoke(position);
    }

    private bool IsEnemyTank(int element) => 6<=element && element<=9;
}
