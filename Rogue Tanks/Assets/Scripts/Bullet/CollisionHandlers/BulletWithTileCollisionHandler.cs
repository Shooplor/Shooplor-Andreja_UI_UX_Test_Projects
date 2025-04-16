using UnityEngine;

public class BulletWithTileCollisionHandler : ICollisionHandler
{
    private readonly BulletController bulletController;
    public string CollisionTag => "Tile";
    public BulletWithTileCollisionHandler(BulletController bulletController) => this.bulletController = bulletController;

    public void Execute(Collider2D collision)
    {
        bulletController.Deactivate();
        var unitTile = collision.gameObject.GetComponent<UnitTile>();
        if(unitTile != null)
        {
            unitTile.Hit(bulletController);
        }
    }
}
