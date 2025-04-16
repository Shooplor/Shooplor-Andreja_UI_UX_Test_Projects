using UnityEngine;

public class BulletSprite : MonoBehaviour
{
    private BulletCollisionEngine bulletCollisionEngine;
    public BulletSprite Initialize(BulletController bulletController)
    {
        bulletCollisionEngine = BulletCollisionEngine.Create(new BulletWithGroundCollisionHandler(bulletController), 
            new BulletWithTankCollisionHandler(bulletController), new BulletWithBulletCollisionHandler(bulletController),
            new BulletWithTileCollisionHandler(bulletController), new BulletWithBaseCollisionHandler(bulletController));
        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision) => bulletCollisionEngine.HandleCollision(collision);
    
    public void FaceUp() => transform.rotation = Quaternion.Euler(0, 0, 0);
    public void FaceDown() => transform.rotation = Quaternion.Euler(0, 0, 180);
    public void FaceLeft() => transform.rotation = Quaternion.Euler(0, 0, 90);
    public void FaceRight() => transform.rotation = Quaternion.Euler(0, 0, -90);
}
