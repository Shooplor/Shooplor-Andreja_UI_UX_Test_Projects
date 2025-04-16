using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTank
{
    protected GameObject tankGameObject;
    protected Tank tank;
    protected GameEngine gameEngine;
    protected SpriteController spriteController;
    protected SpriteRender spriteRender;
    protected TankMovementController tankMovementController;
    protected WeaponController weaponController;
    protected TankStats tankStats;

    private bool hasArmor = false;

    public BaseTank(GameObject tank, GameEngine gameEngine)
    {
        this.tankGameObject = tank;
        this.tank = tank.GetComponent<Tank>();
        this.gameEngine = gameEngine;
        var sprite = tank.transform.Find("Sprite");
        spriteController = sprite.GetComponent<SpriteController>();
        gameEngine.GameFactory.CreateRaycasts(spriteController.transform.position, spriteController.transform, raycasts => spriteController.Raycasts = raycasts);
        spriteRender = sprite.GetComponent<SpriteRender>();
        tankStats = tank.GetComponentInChildren<TankStats>();
        weaponController = tank.GetComponent<WeaponController>();
        weaponController.Initialize(gameEngine, spriteController, tank.GetComponent<Tank>());
        tankMovementController = tank.GetComponent<TankMovementController>();
        tankMovementController.SpriteController = spriteController;
        tankMovementController.TankStats = tankStats;

        spriteController.Raycasts.SetCollisionMasks("Ground", "Player", "Enemy", "Tile", "WaterTile");
    }

    public abstract void CanGetUpgrade(Action onCanGetUpgrade);
    public abstract void DestroyTank();
    public abstract void TankIsHit(Action<int> onHit);
    public abstract void AddPoints(int destroyedTankType);
    public void BulletHit(Action<int> onHit) => hasArmor.OnFalse(()=>TankIsHit(onHit));
    public void DisableTank()
    {
        tankMovementController.Movement.Disable();
        weaponController.movementController.Disable();
    }

    public void EnableArmor() => hasArmor = true;
    public void DisableArmor() => hasArmor = false;
    public void EnableTank()
    {
        tankMovementController.Movement.Enable();
        weaponController.movementController.Enable();
    }

    public void AddPointsForTank(int destroyedTankType) => AddPoints(destroyedTankType);

    internal Dictionary<int, int> GetTankPoints() => tankStats.TankPoints;

    public void SpawnAt(Vector3 position, int seconds, Action<SpriteController> onSpawn)=>
        ActivateAfter(seconds, position, () => onSpawn(spriteController));

    public BaseTank Deactivate(Action onDeactivate)
    {
        tank.gameObject.SetActive(false);
        onDeactivate();
        return this;
    }

    public BaseTank ActivateAfter(int seconds, Vector3 position, Action onActive)
    {
        gameEngine.GameUtilities.WaitFor(seconds, () => tank.gameObject.SetActiveAt(position));
        onActive();
        return this;
    }
}
