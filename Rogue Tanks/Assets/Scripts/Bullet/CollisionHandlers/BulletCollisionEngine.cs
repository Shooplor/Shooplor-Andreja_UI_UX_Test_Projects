using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionEngine
{
    private readonly Dictionary<string, ICollisionHandler> collisions;

    private BulletCollisionEngine() => collisions = new Dictionary<string, ICollisionHandler>();

    private BulletCollisionEngine(params ICollisionHandler[] collisions): this() => collisions.ForEach(AddCollisionHandler);

    public void AddCollisionHandler(ICollisionHandler collisionHandler) => collisions.Add(collisionHandler.CollisionTag, collisionHandler);
    
    public void HandleCollision(Collider2D collision) => collisions.GetValue(collision.tag, collisionAction => collisionAction.Execute(collision));

    public static BulletCollisionEngine CreateEmpty() => new BulletCollisionEngine();
    public static BulletCollisionEngine Create(params ICollisionHandler[] collisionHandlers) => new BulletCollisionEngine(collisionHandlers);
}
