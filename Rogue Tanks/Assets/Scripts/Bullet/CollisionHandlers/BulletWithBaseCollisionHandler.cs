using UnityEngine;

public class BulletWithBaseCollisionHandler : ICollisionHandler
{
    private readonly BulletController bulletController;
    public string CollisionTag => "Base";
    public BulletWithBaseCollisionHandler(BulletController bulletController) => this.bulletController = bulletController;

    public void Execute(Collider2D collision)
    {
        bulletController.Deactivate();
        collision.gameObject.GetComponentInParent<TankBase>().BulletHit();
    }
}
