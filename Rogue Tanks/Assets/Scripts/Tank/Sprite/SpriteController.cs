using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public RaycastController Raycasts { get; set; }
    
    internal SpriteController FaceUp()
    {
        Raycasts.LookUp();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        return this;
    }

    internal SpriteController FaceDown()
    {
        Raycasts.LookDown();
        transform.rotation = Quaternion.Euler(0, 0, 180);
        return this;
    }

    internal SpriteController FaceLeft()
    {
        Raycasts.LookLeft();
        transform.rotation = Quaternion.Euler(0, 0, 90);
        return this;
    }

    internal SpriteController FaceRight()
    {
        Raycasts.LookRight();
        transform.rotation = Quaternion.Euler(0, 0, -90);
        return this;
    }
}
