using UnityEngine;

public class BulletWithTankCollisionHandler : ICollisionHandler
{
    private readonly BulletController bulletController;
    public string CollisionTag => "TankStats";

    public BulletWithTankCollisionHandler(BulletController bulletController) =>
        this.bulletController = bulletController;

    public void Execute(Collider2D collision) =>
        GetTank(collision)
            .TankIdentifier
            .HitsAnotherTank(bulletController.Tank.TankIdentifier, bulletController.Deactivate)
            .HitsTankFromAnotherGroup(bulletController.Tank.TankIdentifier, () => GetTank(collision).BulletHit(tankType => bulletController.Tank.AddPoints(tankType)));

    private Tank GetTank(Collider2D collision) => collision.gameObject.GetComponentInParent<Tank>();
}
