using UnityEngine;

public class TimeStopUpgrade : IUpgradeType
{
    private readonly GameEngine gameEngine;
    private readonly int numberOfSeconds = 3;
    public TimeStopUpgrade(GameEngine gameEngine, int numberOfSeconds)
    {
        this.gameEngine = gameEngine;
        this.numberOfSeconds = numberOfSeconds;
    }
    public void Upgrade(GameObject tank)
    {
        gameEngine.ActiveGameObjectsController.ActiveTanks.DisableAllEnemyTanks();
        gameEngine.GameUtilities.WaitFor(numberOfSeconds, () => gameEngine.ActiveGameObjectsController.ActiveTanks.EnableAllEnemyTanks());
    }
}
