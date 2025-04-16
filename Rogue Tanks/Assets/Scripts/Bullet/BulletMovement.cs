using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float Speed = 5.0f;
    private Vector3 fireDirection = Vector3.up;
    public Vector3 Direction { get => fireDirection; }

    private void FixedUpdate() => transform.Translate(fireDirection * Speed * Time.deltaTime);

    public void FaceUp() => fireDirection = Vector3.up;

    public void FaceDown() => fireDirection = Vector3.down;

    public void FaceLeft() => fireDirection = Vector3.left;

    public void FaceRight() => fireDirection = Vector3.right;
}
