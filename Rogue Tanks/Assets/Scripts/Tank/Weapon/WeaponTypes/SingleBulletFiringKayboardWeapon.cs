using System;
using System.Collections.Generic;
using UnityEngine;

public class SingleBulletFiringKayboardWeapon : MonoBehaviour, IWeaponType
{
    private readonly SpriteController spriteController;
    private readonly Tank tank;

    public List<GameObject> Bullets { get; private set; }
    private BulletController bulletController;
    public SingleBulletFiringKayboardWeapon(GameEngine gameEngine, SpriteController spriteController, Tank tank,float bulletSpeed)
    {
        Bullets = new List<GameObject>();
        Bullets.Add(gameEngine.GameFactory.CreateBullet(tank.transform.position,tank,bulletSpeed, bc=>bulletController = bc));
        this.spriteController = spriteController;
        this.tank = tank;
    }

    public void CanFire(Action onCanFire)
    {
        if (bulletController.gameObject.activeSelf) return;
        onCanFire();
    }

    public void Fire()
    {
        bulletController.SetActiveAt(tank.transform.position);
        spriteController.Raycasts.LookDirection
            .FacesUp(bulletController.FaceUp)
            .FacesDown(bulletController.FaceDown)
            .FacesLeft(bulletController.FaceLeft)
            .FacesRight(bulletController.FaceRight);
    }
}
