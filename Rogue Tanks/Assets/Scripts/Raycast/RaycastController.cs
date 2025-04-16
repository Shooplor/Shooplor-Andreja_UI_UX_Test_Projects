using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    private List<Raycast> raycastList;
    public Vector2 LookDirection { get => raycastList[0].LookDirection; }
    public void SetCollisionMasks(params string[] collisionMasks) => raycastList = transform.GetAllComponents<Raycast>().Select(raycast => raycast.SetCollisionLayerMasks(collisionMasks));

    public void LookUp() => raycastList.ForEach(raycast => raycast.LookUp());
    public void LookDown() => raycastList.ForEach(raycast => raycast.LookDown());
    public void LookLeft() => raycastList.ForEach(raycast => raycast.LookLeft());
    public void LookRight() => raycastList.ForEach(raycast => raycast.LookRight());

    private bool hasDetectedCollision { get => raycastList.Any(raycast => raycast.HasDetectedCollision); }

    private bool HasDetectedCollisionInDirectionOf(Vector2 moveDirection) => hasDetectedCollision && moveDirection == LookDirection;

    public void WhenThereIsNoCollisionDetected(Action onNoCollision)
    {
        if (hasDetectedCollision) return;
        onNoCollision();
    }

    public void CanMoveInDirectionOf(Vector2 moveDirection, Action onCanMove)
    {
        if (HasDetectedCollisionInDirectionOf(moveDirection)) return;
        onCanMove();
    }

    public void CanMoveInDirectionOf(Vector2 moveDirection, Action onCanMove, Action onCannotMove)
    {
        if (HasDetectedCollisionInDirectionOf(moveDirection))
            onCannotMove();
        else
            onCanMove();
    }
}
