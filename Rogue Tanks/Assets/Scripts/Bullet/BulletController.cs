using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Bullet bullet;
    public Tank Tank { get; private set; }
    private int bulletStrength = 1;
    
    public BulletController InitializeBullet(Tank tank, float bulletSpeed)
    {
        Tank = tank;
        bullet = new Bullet(GetComponent<BulletMovement>(), GetComponentInChildren<BulletSprite>().Initialize(this));
        bullet.Speed = bulletSpeed;
        gameObject.SetActive(false);
        return this;
    }

    public Vector3 Direction { get => bullet.Direction; }
    public int Strength { get => bulletStrength; }
    public void IncreaseBulletStrength() => bulletStrength++;
    public bool BulletCanDestroyTile(int tileStrength) => bulletStrength >= tileStrength;
    public void FaceUp() => bullet.FaceUp();
    public void FaceDown() => bullet.FaceDown();
    public void FaceLeft() => bullet.FaceLeft();
    public void FaceRight() => bullet.FaceRight();
    public void SetActiveAt(Vector3 position) => gameObject.SetActiveAt(position);
    public void Deactivate() => gameObject.SetActive(false);
}
