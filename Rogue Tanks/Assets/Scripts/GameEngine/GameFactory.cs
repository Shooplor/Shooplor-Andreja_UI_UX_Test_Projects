using System;
using UnityEngine;
using UnityEngine.UI;

public class GameFactory : MonoBehaviour
{
    private GameEngine gameEngine;
    private void Awake() => gameEngine = GetComponent<GameEngine>();

    public GameObject TankPrefab;
    public GameObject BulletPrefab;
    public GameObject UpgradePrefab;
    public GameObject TankBasePrefab;
    public GameObject RaycastsPrefab;
    public GameObject ScoreboardTankPrefab;
    public GameObject GroupTilePrefab;
    public GameObject SingleTilePrefab;

    public Text Lives;
    public Text Level;

    public Sprite BrokenEagle;
    public Sprite[] BrickSprites;
    public Sprite[] ForestSprites;
    public Sprite[] WaterSprites;
    public Sprite[] StoneWallSprites;

    public Sprite ArmorUpgradeSprite;
    public Sprite BombUpgradeSprite;
    public Sprite PowerUpSprite;
    public Sprite ShovelUpgradeSprite;
    public Sprite StarUpgradeSprite;
    public Sprite TimeStopUpgradeSprite;
    public Sprite IceSprite;
    public Sprite LavaSprite;
    public Sprite MudSprite;

    public void CreateTank(Vector3 startingPosition, Action<Tank> onTankCreated)
    {
        var tank = Instantiate(TankPrefab, startingPosition.AdjustForTank(), Quaternion.identity);
        var tankScript = tank.GetComponent<Tank>();
        onTankCreated(tankScript);
        gameEngine.ActiveGameObjectsController.AddTank(tankScript);
    }

    public GameObject CreateBullet(Vector3 position,Tank tank, float bulletSpeed, Action<BulletController> onBulletCreated)
    {
        var bullet = Instantiate(BulletPrefab,position, Quaternion.identity);
        var bulletController = bullet.GetComponent<BulletController>().InitializeBullet(tank, bulletSpeed);
        onBulletCreated(bulletController);
        return bullet;
    }

    public void CreateRaycasts(Vector3 position, Transform parent, Action<RaycastController> onRaycastsCreated)
    {
        var raycasts = Instantiate(RaycastsPrefab, position, Quaternion.identity, parent);
        onRaycastsCreated(raycasts.GetComponent<RaycastController>());
    }

    public void CreateTankBase(Vector3 position, Action<TankBase> onTankBase)
    {
        var tankBase = Instantiate(TankBasePrefab, position.AdjustForTank(), Quaternion.identity);
        var tankBaseScript = tankBase.GetComponent<TankBase>();
        tankBaseScript.Initialize(gameEngine);
        onTankBase(tankBaseScript);
        gameEngine.ActiveGameObjectsController.SetTankBase(tankBaseScript);
    }

    public Upgrade CreateUpgrade(Vector3 position)
    {
        var upgrade = Instantiate(UpgradePrefab, position, Quaternion.identity);
        return upgrade.GetComponent<Upgrade>();
    }

    public GameObject CreateScoreboardTank(Vector3 position) => Instantiate(ScoreboardTankPrefab, position, Quaternion.identity);

    public GroupTile CreateGroupTile(Vector3 position,int row, int column, Action<GroupTile> onTile)
    {
        var groupTile = Instantiate(GroupTilePrefab, position, Quaternion.identity);
        groupTile.name = groupTile.name + $"_{row}_{column}";
        var groupTileScript = groupTile.GetComponent<GroupTile>();
        onTile(groupTileScript);
        gameEngine.GameTiles.Add(groupTileScript);
        return groupTileScript;
    }

    public GroupTile CreateBrick(Vector3 position, int row, int column) => CreateGroupTile(position, row, column, groupTile => groupTile.Initialize(BrickSprites.Get(0,1,4,5), "Tile",1));
    public GroupTile CreateForest(Vector3 position, int row, int column) => CreateGroupTile(position, row, column, groupTile => groupTile.Initialize(ForestSprites.Get(0, 1, 4, 5), "ForestTile",0));
    public GroupTile CreateWater(Vector3 position, int row, int column) => CreateGroupTile(position, row, column, groupTile => groupTile.Initialize(WaterSprites.Get(0, 1, 4, 5), "WaterTile",0));
    public GroupTile CreateStone(Vector3 position, int row, int column) => CreateGroupTile(position, row, column, groupTile => groupTile.Initialize(StoneWallSprites.Get(0, 1, 4, 5), "Tile",2));
    public SingleTile CreateIce(Vector3 position, int row, int column)
    {
        var singleTile = Instantiate(SingleTilePrefab, position.AdjustForTank(), Quaternion.identity);
        singleTile.name = singleTile.name + $"_{row}_{column}";
        var singleTileScript = singleTile.GetComponent<SingleTile>();
        singleTileScript.Initialize(IceSprite, "IceTile");
        gameEngine.GameTiles.Add(singleTileScript);
        return singleTileScript;
    }
    public SingleTile CreateLava(Vector3 position, int row, int column)
    {
        var singleTile = Instantiate(SingleTilePrefab, position.AdjustForTank(), Quaternion.identity);
        singleTile.name = singleTile.name + $"_{row}_{column}";
        var singleTileScript = singleTile.GetComponent<SingleTile>();
        singleTileScript.Initialize(LavaSprite, "LavaTile");
        gameEngine.GameTiles.Add(singleTileScript);
        return singleTileScript;
    }
    public SingleTile CreateMud(Vector3 position, int row, int column)
    {
        var singleTile = Instantiate(SingleTilePrefab, position.AdjustForTank(), Quaternion.identity);
        singleTile.name = singleTile.name + $"_{row}_{column}";
        var singleTileScript = singleTile.GetComponent<SingleTile>();
        singleTileScript.StatEffects = TileStatEffects.Instance.GetStatEffectsForTile("Mud");
        singleTileScript.Initialize(MudSprite, "MudTile");
        gameEngine.GameTiles.Add(singleTileScript);
        return singleTileScript;
    }
}
