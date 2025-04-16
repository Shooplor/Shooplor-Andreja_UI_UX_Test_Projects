using UnityEngine;

public class ArmorUpgrade : IUpgradeType
{
    private readonly GameUtilities gameEngine;
    private readonly int activeSeconds;
    public ArmorUpgrade(GameUtilities gameEngine, int activeSeconds)
    {
        this.gameEngine = gameEngine;
        this.activeSeconds = activeSeconds;
    }
    public void Upgrade(GameObject tank)
    {
        tank.GetComponent<Tank>().EnableArmor();
        gameEngine.WaitFor(activeSeconds, tank.GetComponent<Tank>().DisableArmor);
    }
}
