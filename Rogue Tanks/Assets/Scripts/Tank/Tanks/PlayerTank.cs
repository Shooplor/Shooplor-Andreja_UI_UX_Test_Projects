using System;
using UnityEngine;

public class PlayerTank : BaseTank
{
    public PlayerTank(GameObject tank, GameEngine gameEngine) : base(tank, gameEngine)
    {
        new TankBuilder(gameEngine, base.tank, tankMovementController, weaponController, tankStats, spriteRender, spriteController)
        .BuildPlayer();
        gameEngine.ScoreboardController.DisplayLives(tankStats.Lives);
    }

    public override void CanGetUpgrade(Action onCanGetUpgrade) => onCanGetUpgrade();

    public override void DestroyTank()
    {
        GameStatistics.AddStatistics(tankStats.TankPoints.Copy());
        gameEngine.ActiveGameObjectsController.ActiveTanks.Destroy(tank,gameEngine.GameEvents.RaiseOnPlayerTankDestroyed);
    }

    public override void TankIsHit(Action<int> onHit) =>
        tankStats.DecreaseLife(lives => gameEngine.ScoreboardController.DisplayLives(lives))
                 .TankStillHaveLivesLeft(RespawnTank)
                 .TankHasNoMoreLivesLeft(DestroyTank);

    private void RespawnTank() =>
        Deactivate(()=> { ResetWeapon(); ResetMovement(); })
        .ActivateAfter(1, gameEngine.LevelController.CurrentLevel.PlayerTankPosition,()=>spriteController.FaceUp());

    private void ResetWeapon() => weaponController.SetWeaponType(new SingleBulletFiringKayboardWeapon(gameEngine, spriteController, base.tank, 30.0f));
    private void ResetMovement() => tankMovementController.ResetMovement();

    public override void AddPoints(int destroyedTankType) => tankStats.AddPoint(destroyedTankType);
}
