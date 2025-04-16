using UnityEngine;

public class PowerUpUpgrade : IUpgradeType
{
    private readonly GameEngine gameEngine;
    public PowerUpUpgrade(GameEngine gameEngine)
    {
        this.gameEngine = gameEngine;
    }
    public void Upgrade(GameObject tank)=>
        tank.GetComponent<Tank>().GetComponentInChildren<TankStats>().AddLive(1, gameEngine.ScoreboardController.DisplayLives);
}
