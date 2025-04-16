using UnityEngine;

public interface ICollisionHandler
{
    string CollisionTag { get; }
    void Execute(Collider2D collision);
}
