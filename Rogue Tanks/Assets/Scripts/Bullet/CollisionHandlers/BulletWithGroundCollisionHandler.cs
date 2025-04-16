using UnityEngine;

public class BulletWithGroundCollisionHandler : ICollisionHandler
{
    private readonly BulletController bulletController;
    public string CollisionTag => "Ground";

    public BulletWithGroundCollisionHandler(BulletController bullet) => this.bulletController = bullet;

    public void Execute(Collider2D collision) => bulletController.Deactivate();
}
