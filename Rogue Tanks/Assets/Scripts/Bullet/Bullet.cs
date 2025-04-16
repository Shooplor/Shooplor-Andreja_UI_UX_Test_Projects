using UnityEngine;

public class Bullet
{
    private readonly BulletMovement movement;
    private readonly BulletSprite sprite;

    public float Speed { get => movement.Speed; internal set => movement.Speed = value; }
    public Vector3 Direction { get => movement.Direction; }

    public Bullet(BulletMovement movement, BulletSprite sprite)
    {
        this.movement = movement;
        this.sprite = sprite;
    }

    public void FaceUp()
    {
        movement.FaceUp();
        sprite.FaceUp();
    }

    public void FaceDown()
    {
        movement.FaceDown();
        sprite.FaceDown();
    }

    public void FaceLeft()
    {
        movement.FaceLeft();
        sprite.FaceLeft();
    }

    public void FaceRight()
    {
        movement.FaceRight();
        sprite.FaceRight();
    }
}
