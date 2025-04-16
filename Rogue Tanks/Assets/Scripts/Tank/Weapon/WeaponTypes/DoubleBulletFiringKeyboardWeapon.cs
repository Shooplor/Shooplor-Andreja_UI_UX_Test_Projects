using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoubleBulletFiringKeyboardWeapon : MonoBehaviour, IWeaponType
{
    private readonly SpriteController spriteController;
    private readonly Tank tank;

    public List<GameObject> Bullets { get; private set; }
    private List<BulletController> bulletControllers;

    public DoubleBulletFiringKeyboardWeapon(GameEngine gameEngine, SpriteController spriteController, Tank tank, float bulletSpeed)
    {
        Bullets = new List<GameObject>();
        bulletControllers = new List<BulletController>();

        Bullets.Add(gameEngine.GameFactory.CreateBullet(tank.transform.position, tank, bulletSpeed, bulletControllers.Add));
        Bullets.Add(gameEngine.GameFactory.CreateBullet(tank.transform.position, tank, bulletSpeed, bulletControllers.Add));

        this.spriteController = spriteController;
        this.tank = tank;
    }

    public void CanFire(Action onCanFire)
    {
        if (bulletControllers.All(bullet => bullet.gameObject.activeSelf)) return;
        onCanFire();
    }

    public void Fire()
    {
        var bulletController = bulletControllers.FirstOrDefault(bullet => !bullet.gameObject.activeSelf);
        bulletController.SetActiveAt(tank.transform.position);
        spriteController.Raycasts.LookDirection
            .FacesUp(bulletController.FaceUp)
            .FacesDown(bulletController.FaceDown)
            .FacesLeft(bulletController.FaceLeft)
            .FacesRight(bulletController.FaceRight);
    }
}
