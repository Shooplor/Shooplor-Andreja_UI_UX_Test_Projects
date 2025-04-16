using System;
using UnityEngine;

public class EnemyTank : BaseTank
{
    public EnemyTank(GameObject tank, GameEngine gameEngine, int tankType) : base(tank, gameEngine) =>
        new TankBuilder(gameEngine, base.tank, tankMovementController, weaponController, tankStats, spriteRender, spriteController)
        .BuildEnemy(tankType);

    public override void AddPoints(int destroyedTankType) { }
    public override void CanGetUpgrade(Action onCanGetUpgrade) { }

    public override void DestroyTank() => gameEngine.ActiveGameObjectsController.DestroyTank(tank);

    public override void TankIsHit(Action<int> onHit) =>
        tankStats.DecreaseLife(()=>onHit(tankStats.TankType))
        .TankHasNoMoreLivesLeft(DestroyTank);
}
