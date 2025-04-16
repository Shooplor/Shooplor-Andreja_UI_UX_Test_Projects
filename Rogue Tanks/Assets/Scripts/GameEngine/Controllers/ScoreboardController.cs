using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreboardController
{
    private readonly GameEngine gameEngine;
    private List<GameObject> scoreboardTanks;
    public ScoreboardController(GameEngine gameEngine)
    {
        this.gameEngine = gameEngine;
        scoreboardTanks = new List<GameObject>();
    }

    public void CreateScoreboardTanks(int numberOfTanks)
    {
        int y = 23;
        for(int i = 0; i < numberOfTanks; i += 2)
        {
            scoreboardTanks.Add(gameEngine.GameFactory.CreateScoreboardTank(new Vector3(30, y, 0)));
            if (numberOfTanks- i >1)
                scoreboardTanks.Add(gameEngine.GameFactory.CreateScoreboardTank(new Vector3(33, y, 0)));
            y -= 3;
        }
    }

    public void RemoveScoreboardTank()
    {
        var scoreBoardTankLast = scoreboardTanks.Last();
        GameObject.Destroy(scoreBoardTankLast.gameObject);
        scoreboardTanks.Remove(scoreBoardTankLast);
    }

    public void DisplayLives(int lives) => gameEngine.GameFactory.Lives.text = lives.ToString();

    public void DisplayLevel(int level) => gameEngine.GameFactory.Level.text = level.ToString();
}
