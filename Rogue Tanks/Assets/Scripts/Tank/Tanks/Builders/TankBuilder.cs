using System;
using System.Collections.Generic;
using UnityEngine;

public class TankBuilder
{
    private readonly GameEngine gameEngine;
    private readonly Tank tank;
    private readonly TankMovementController tankMovementController;
    private readonly WeaponController weaponController;
    private readonly SpriteRender spriteRender;
    private readonly TankStats tankStats;
    private readonly SpriteController spriteController;

    private readonly Dictionary<int, Action> builder;

    public TankBuilder(GameEngine gameEngine, Tank tank, TankMovementController tankMovementController, WeaponController weaponController, TankStats tankStats, SpriteRender spriteRender, SpriteController spriteController)
    {
        this.gameEngine = gameEngine;
        this.tank = tank;
        this.tankMovementController = tankMovementController;
        this.weaponController = weaponController;
        this.spriteRender = spriteRender;
        this.tankStats = tankStats;
        this.spriteController = spriteController;

        this.builder = new Dictionary<int, Action>();
        builder.Add(0, CreateEnemyTank1);
        builder.Add(1, CreateEnemyTank2);
        builder.Add(2, CreateEnemyTank3);
        builder.Add(3, CreateEnemyTank4);
    }

    public void BuildEnemy(int tankId)
    {
        CreateEnemyTankBasics();
        builder[tankId].Invoke();
    }

    public void BuildPlayer()
    {
        tank.gameObject.layer = LayerMask.NameToLayer("Player");
        spriteRender.CreatePlayerTank();
        tankMovementController.Movement = new KeyboardInputActions(tankMovementController.transform);
        tankMovementController.BaseSpeed = 10.0f;
        tankMovementController.SpriteController.FaceUp();
        weaponController.SetWeaponType(new SingleBulletFiringKayboardWeapon(gameEngine, spriteController, tank, 30.0f));
        weaponController.movementController = new KeyboardInputActions(tankMovementController.transform);
        tankStats.Initialize(5, 3);
    }

    private void CreateEnemyTankBasics()
    {
        tank.gameObject.layer = LayerMask.NameToLayer("Enemy");
        tankMovementController.Movement = new RandomInputActions();
        weaponController.SetWeaponType(new SingleBulletRandomFiringWeapon(gameEngine, spriteController, tank, 20.0f));
        weaponController.movementController = new RandomInputActions();
    }

    private void CreateEnemyTank1()
    {
        spriteRender.CreateEnemyTank1();
        tankMovementController.BaseSpeed = 5.0f;
        tankStats.Initialize(6, 1);
        spriteController.FaceDown();
    }

    private void CreateEnemyTank2()
    {
        spriteRender.CreateEnemyTank2();
        tankMovementController.BaseSpeed = 13.0f;
        tankStats.Initialize(7, 1);
        spriteController.FaceDown();
    }

    private void CreateEnemyTank3()
    {
        spriteRender.CreateEnemyTank3();
        tankMovementController.BaseSpeed = 10.0f;
        weaponController.UpgradeWeapon();
        tankStats.Initialize(8, 1);
        spriteController.FaceDown();
    }

    private void CreateEnemyTank4()
    {
        spriteRender.CreateEnemyTank4();
        tankMovementController.BaseSpeed = 10.0f;
        tankStats.Initialize(9, 4);
        spriteController.FaceDown();
    }
}
