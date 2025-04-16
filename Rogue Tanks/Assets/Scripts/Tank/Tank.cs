using System;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private BaseTank baseTank;
    private Identifier tankIdentifier;
    public Identifier TankIdentifier { get => tankIdentifier; }

    public void IsEnemyTank(Action onEnemyTank) => baseTank.Is<EnemyTank>(onEnemyTank);

    public bool IsEnemyTank() => baseTank is EnemyTank;
    public bool IsPlayerTank() => baseTank is PlayerTank;

    public void CreatePlayerTank(string groupID, GameEngine gameEngine)
    {
        tankIdentifier = Identifier.Create(groupID);
        baseTank = new PlayerTank(gameObject, gameEngine);
    }

    public void CreateEnemyTank1(string groupID, GameEngine gameEngine)
    {
        tankIdentifier = Identifier.Create(groupID);
        baseTank = new EnemyTank(gameObject, gameEngine, 0);
    }

    public void CreateEnemyTank2(string groupID, GameEngine gameEngine)
    {
        tankIdentifier = Identifier.Create(groupID);
        baseTank = new EnemyTank(gameObject, gameEngine, 1);
    }

    public void CreateEnemyTank3(string groupID, GameEngine gameEngine)
    {
        tankIdentifier = Identifier.Create(groupID);
        baseTank = new EnemyTank(gameObject, gameEngine, 2);
    }

    public void CreateEnemyTank4(string groupID, GameEngine gameEngine)
    {
        tankIdentifier = Identifier.Create(groupID);
        baseTank = new EnemyTank(gameObject, gameEngine, 3);
    }

    public void Destroy() => baseTank.DestroyTank();
    public void CanGetUpgrade(Action onCanGetUpgrade) => baseTank.CanGetUpgrade(onCanGetUpgrade);
    public void BulletHit(Action<int> onHit) => baseTank.BulletHit(onHit);
    public void DisableTank() => baseTank.DisableTank();
    public void EnableTank() => baseTank.EnableTank();
    public void EnableArmor() => baseTank.EnableArmor();
    public void DisableArmor() => baseTank.DisableArmor();
    public void SpawnAt(Vector3 position, int seconds, Action<SpriteController> onSpawn) => baseTank.SpawnAt(position, seconds, onSpawn);
    public void AddPoints(int destroyedTankType) => baseTank.AddPointsForTank(destroyedTankType);

    public Dictionary<int, int> GetTankPoints() => baseTank.GetTankPoints();
}
