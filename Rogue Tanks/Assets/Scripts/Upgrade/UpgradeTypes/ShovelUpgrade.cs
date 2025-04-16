using UnityEngine;

public class ShovelUpgrade : IUpgradeType
{
    private readonly GameEngine gameEngine;
    private readonly int numberOfSeconds;
    public ShovelUpgrade(GameEngine gameEngine, int numberOfSeconds)
    {
        this.gameEngine = gameEngine;
        this.numberOfSeconds = numberOfSeconds;
    }
    public void Upgrade(GameObject tank)
    {
        gameEngine.ActiveGameObjectsController.TankBase.ProtectBase();
        gameEngine.GameUtilities.CountFor(10, Execute);
    }

    private void Execute(int second)
    {
        UnprotectBase(second);
        StartBlinking(second);
    }

    private void UnprotectBase(int second) => second.Equals(numberOfSeconds, gameEngine.ActiveGameObjectsController.TankBase.UnprotectBase);
    private void StartBlinking(int second) => second.Equals(numberOfSeconds-3, ()=> gameEngine.ActiveGameObjectsController.TankBase.Blink(6));
}
