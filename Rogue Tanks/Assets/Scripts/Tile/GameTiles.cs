using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameTiles : MonoBehaviour
{
    private List<GroupTile> GroupTiles = new List<GroupTile>();
    private List<SingleTile> SingleTiles = new List<SingleTile>();

    public void Add(GroupTile groupTile)
    {
        groupTile.OnHit += Hit;
        GroupTiles.Add(groupTile);
    }
    public void Add(SingleTile singleTile)
    {
        SingleTiles.Add(singleTile);
    }

    public void Reset()
    {
        GroupTiles.ForEach(DestroyGroupTile);
        GroupTiles.Clear();
        SingleTiles.ForEach(DestroySingleTile);
        SingleTiles.Clear();
    }

    private void DestroyGroupTile(GroupTile groupTile)
    {
        groupTile.OnHit -= Hit;
        GameObject.Destroy(groupTile.gameObject);
    }
    private void DestroySingleTile(SingleTile singleTile)
    {
        GameObject.Destroy(singleTile.gameObject);
    }

    private void Hit(BulletController bullet, GroupTile groupTile, UnitTile unitTile)
    {
        if (bullet.Strength < groupTile.Strength) return;
        bullet.Direction
            .OnVertical(() =>
            {
                groupTile.RemoveUnitTile(unitTile.Row, unitTile.Column);
                if (unitTile.Column == 1)
                {
                    groupTile.RemoveUnitTile(unitTile.Row, unitTile.Column + 1);
                    var leftGroupTile = GroupTiles.SingleOrDefault(gt => gt.Row == groupTile.Row && gt.Column == groupTile.Column - 1);
                    if (leftGroupTile != null && bullet.Strength >= leftGroupTile.Strength)
                        leftGroupTile.RemoveUnitTile(unitTile.Row, 2);
                }
                if (unitTile.Column == 2)
                {
                    groupTile.RemoveUnitTile(unitTile.Row, unitTile.Column - 1);
                    var rightGroupTile = GroupTiles.SingleOrDefault(gt => gt.Row == groupTile.Row && gt.Column == groupTile.Column + 1);
                    if (rightGroupTile != null && bullet.Strength >= rightGroupTile.Strength)
                        rightGroupTile.RemoveUnitTile(unitTile.Row, 1);
                }
            })
            .OnHorizontal(() =>
            {
                groupTile.RemoveUnitTile(unitTile.Row, unitTile.Column);
                if (unitTile.Row == 1)
                {
                    groupTile.RemoveUnitTile(unitTile.Row + 1, unitTile.Column);
                    var downGroupTile = GroupTiles.SingleOrDefault(gt => gt.Row == groupTile.Row - 1 && gt.Column == groupTile.Column);
                    if (downGroupTile != null && bullet.Strength >= downGroupTile.Strength)
                        downGroupTile.RemoveUnitTile(2, unitTile.Column);
                }
                if (unitTile.Row == 2)
                {
                    groupTile.RemoveUnitTile(unitTile.Row - 1, unitTile.Column);
                    var topGroupTile = GroupTiles.SingleOrDefault(gt => gt.Row == groupTile.Row + 1 && gt.Column == groupTile.Column);
                    if (topGroupTile != null && bullet.Strength >= topGroupTile.Strength)
                        topGroupTile.RemoveUnitTile(1, unitTile.Column);
                }

            });
    }
}
