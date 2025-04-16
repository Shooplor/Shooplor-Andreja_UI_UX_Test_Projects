using UnityEngine;

public class BulletWithBulletCollisionHandler : ICollisionHandler
{
    private readonly BulletController bulletController;
    public string CollisionTag => "Bullet";

    public BulletWithBulletCollisionHandler(BulletController bulletController) => this.bulletController = bulletController;

    public void Execute(Collider2D collision) => bulletController.Deactivate();
}
