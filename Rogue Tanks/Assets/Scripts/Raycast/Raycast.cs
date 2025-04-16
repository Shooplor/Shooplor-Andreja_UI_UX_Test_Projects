using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float rayLength;
    private int collisionLayerMask = -1;
    private RaycastHit2D centerCheckHit;

    public Vector2 LookDirection { private set; get; }

    public bool HasDetectedCollision { get => centerCheckHit; }

    public void SetCollisionLayerMasks(params string[] layerMasks) => collisionLayerMask = LayerMask.GetMask(layerMasks);

    void Update() =>
        Physics2D.Raycast(transform.position, LookDirection, rayLength, collisionLayerMask)
        .AssignTo(ref centerCheckHit)
        .HasHitObject(DrawRedRay)
        .NoHit(DrawGreenRay);

    internal void LookUp() => LookDirection = Vector2.up;
    internal void LookDown() => LookDirection = Vector2.down;
    internal void LookLeft() => LookDirection = Vector2.left;
    internal void LookRight() => LookDirection = Vector2.right;

    private void DrawRedRay() => Debug.DrawRay(transform.position, LookDirection * rayLength, Color.red);
    private void DrawGreenRay() => Debug.DrawRay(transform.position, LookDirection * rayLength, Color.green);
}
