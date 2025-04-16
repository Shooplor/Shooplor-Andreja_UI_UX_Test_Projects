using UnityEngine;

public class DestroyAllTanksUpgrade : IUpgradeType
{
    private readonly GameEngine gameEngine;
    public DestroyAllTanksUpgrade(GameEngine gameEngine) => this.gameEngine = gameEngine;
    public void Upgrade(GameObject tank) =>
        gameEngine.ActiveGameObjectsController.ActiveTanks.DestroyAllEnemyTanks();
}
