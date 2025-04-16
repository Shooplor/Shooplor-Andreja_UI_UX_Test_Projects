using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBuilder
{
    private readonly GameEngine gameEngine;
    private readonly IEnumerable<Tank> playerTanks;
    private Dictionary<int, Func<Upgrade>> upgrades = new Dictionary<int, Func<Upgrade>>();

    public UpgradeBuilder(GameEngine gameEngine)
    {
        this.gameEngine = gameEngine;
        this.playerTanks = gameEngine.ActiveGameObjectsController.ActiveTanks.GetPlayerTanks();
        InitializeUpgrades();
    }

    private void InitializeUpgrades()
    {
        upgrades.Add(0, CreateArmorUpgrade);
        upgrades.Add(1, CreateDestroyAllTanksUpgrade);
        upgrades.Add(2, CreatePowerUpUpgrade);
        upgrades.Add(3, CreateShovelUpgrade);
        upgrades.Add(4, CreateStarUpgrade);
        upgrades.Add(5, CreateTimeStopUpgrade);
    }

    public void CreateRandomUpgrade(Action<Upgrade> onUpgradeIsCreated) =>
        Probability.GenerateProbability().IsGreaterThanOrEqualTo(50, () =>
             onUpgradeIsCreated(upgrades.GetRandom().Invoke()));   

    private Vector3 GenerateUpgradePositionAwayFromTank()
    {
        bool isAcceptableDistance = false;
        Vector3 randomUpgradePosition = Vector3.zero;
        while (true)
        {
            randomUpgradePosition = gameEngine.LevelController.CurrentLevel.LevelDimensions.RandomUpgradePosition();
            if (gameEngine.ActiveGameObjectsController.TankBase != null)
            {
                var tankBaseDistance = Math.Abs(Vector3.Distance(gameEngine.ActiveGameObjectsController.TankBase.transform.position, randomUpgradePosition));
                if (tankBaseDistance <= 5) continue;
            }
            foreach(var tank in playerTanks)
            {
                float distance = Math.Abs(Vector3.Distance(tank.transform.position, randomUpgradePosition));
                if (distance >= 8)
                {
                    isAcceptableDistance = true;
                    break;
                }
            }
            if (isAcceptableDistance)
                break;
        }
        return randomUpgradePosition;
    }
    
    public Upgrade CreateArmorUpgrade()=>
        gameEngine.GameFactory.CreateUpgrade(GenerateUpgradePositionAwayFromTank())
        .Initialize(new ArmorUpgrade(gameEngine.GameUtilities, 10), gameEngine.GameFactory.ArmorUpgradeSprite);

    public Upgrade CreateDestroyAllTanksUpgrade() =>
        gameEngine.GameFactory.CreateUpgrade(GenerateUpgradePositionAwayFromTank())
        .Initialize(new DestroyAllTanksUpgrade(gameEngine), gameEngine.GameFactory.BombUpgradeSprite);

    public Upgrade CreatePowerUpUpgrade() =>
        gameEngine.GameFactory.CreateUpgrade(GenerateUpgradePositionAwayFromTank())
        .Initialize(new PowerUpUpgrade(gameEngine), gameEngine.GameFactory.PowerUpSprite);

    public Upgrade CreateShovelUpgrade() =>
        gameEngine.GameFactory.CreateUpgrade(GenerateUpgradePositionAwayFromTank())
        .Initialize(new ShovelUpgrade(gameEngine, 10), gameEngine.GameFactory.ShovelUpgradeSprite);

    public Upgrade CreateStarUpgrade() =>
        gameEngine.GameFactory.CreateUpgrade(GenerateUpgradePositionAwayFromTank())
        .Initialize(new StarUpgrade(), gameEngine.GameFactory.StarUpgradeSprite);

    public Upgrade CreateTimeStopUpgrade() =>
        gameEngine.GameFactory.CreateUpgrade(GenerateUpgradePositionAwayFromTank())
        .Initialize(new TimeStopUpgrade(gameEngine, 10), gameEngine.GameFactory.TimeStopUpgradeSprite);
}
