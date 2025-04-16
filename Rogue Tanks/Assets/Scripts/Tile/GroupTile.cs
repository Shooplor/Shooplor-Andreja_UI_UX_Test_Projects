using System;
using System.Linq;
using UnityEngine;

public class GroupTile : MonoBehaviour
{
    public int Row { get; private set; }
    public int Column { get; private set; }
    public int Strength { get; private set; }
    public bool Active { get => unitTiles.All(ut => ut.gameObject.activeSelf); }
    public event Action<BulletController, GroupTile, UnitTile> OnHit;
    UnitTile[] unitTiles;
    private void Awake() =>
        (unitTiles = GetComponentsInChildren<UnitTile>()).ForEach(unitTile => unitTile.OnHit += Hit);
    
    private void Start()
    {
        Row = name.Split('_')[1].ToInt();
        Column = name.Split('_')[2].ToInt();
    }

    public void Initialize(Sprite[] sprites, string collisionLayer, int strength)
    {
        unitTiles.For((index, unitTile) => unitTile.Initialize(sprites[index], collisionLayer));
        Strength = strength;
    }

    public void RemoveUnitTile(int row, int column) => unitTiles.Single(ut => ut.Row == row && ut.Column == column).gameObject.Deactivate();
    private void Hit(BulletController bullet, UnitTile unitTile) =>OnHit?.Invoke(bullet, this, unitTile);
}
