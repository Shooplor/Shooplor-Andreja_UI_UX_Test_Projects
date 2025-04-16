using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankBase : MonoBehaviour
{
    private GameEngine gameEngine;
    public void Initialize(GameEngine gameEngine) => this.gameEngine = gameEngine;

    private List<GroupTile> tiles = new List<GroupTile>();
    public void BulletHit()
    {
        GameStatistics.AddStatistics(gameEngine.ActiveGameObjectsController.ActiveTanks.GetPlayerTanks().First().GetTankPoints().Copy());
        gameObject.GetComponent<SpriteRenderer>().ApplySprite(gameEngine.GameFactory.BrokenEagle, gameEngine.GameEvents.RaiseOnTankBaseDestroyed);
    }

    private void Start()
    {
        tiles.Add(gameEngine.GameFactory.CreateBrick(gameObject.transform.Offset(-3, -1), 25, 11)); //levo
        tiles.Add(gameEngine.GameFactory.CreateBrick(gameObject.transform.Offset(-3, 1), 24, 11)); //levo
        tiles.Add(gameEngine.GameFactory.CreateBrick(gameObject.transform.Offset(-3, 3), 23, 11)); //levo
        tiles.Add(gameEngine.GameFactory.CreateBrick(gameObject.transform.Offset(-1, 3), 23, 12)); //gore
        tiles.Add(gameEngine.GameFactory.CreateBrick(gameObject.transform.Offset(1, 3), 23, 13)); //gore
        tiles.Add(gameEngine.GameFactory.CreateBrick(gameObject.transform.Offset(3, 3), 23, 14)); //gore
        tiles.Add(gameEngine.GameFactory.CreateBrick(gameObject.transform.Offset(3, 1), 24, 14)); //desno
        tiles.Add(gameEngine.GameFactory.CreateBrick(gameObject.transform.Offset(3, -1), 25, 14)); //desno
    }

    public void Blink(int numberOfTimes) => gameEngine.GameUtilities.RepeatActions(numberOfTimes, 0.5f, new Action[] { ProtectBase, UnprotectBase });
    public void ProtectBase() => tiles.ForEach(groupTile => groupTile.Initialize(gameEngine.GameFactory.StoneWallSprites.Get(0, 1, 4, 5), "Tile", 2));
    public void UnprotectBase() => tiles.ForEach(groupTile => groupTile.Initialize(gameEngine.GameFactory.BrickSprites.Get(0, 1, 4, 5), "Tile", 1));
}
