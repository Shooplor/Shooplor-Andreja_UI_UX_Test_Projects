using System;
using UnityEngine;

public static class SpriteRendererExtensions
{
    public static void ApplySprite(this SpriteRenderer spriteRenderer, Sprite sprite, Action onSpriteApplied)
    {
        spriteRenderer.sprite = sprite;
        onSpriteApplied();
    }
}
