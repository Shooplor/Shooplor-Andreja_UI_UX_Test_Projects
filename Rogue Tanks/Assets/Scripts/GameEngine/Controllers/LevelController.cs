using System;


public class LevelController
{
    private ILevel currentLevel;
    public ILevel CurrentLevel => currentLevel;
    private readonly GameEngine gameEngine;
    private readonly GameObjectBuilder gameObjectBuilder;
    public LevelController(GameEngine gameEngine)
    {
        this.gameEngine = gameEngine;
        gameObjectBuilder = new GameObjectBuilder(gameEngine);
        LevelSelector levelSelector = LevelSelector.Instance;
        currentLevel = new FileLevel(levelSelector != null && levelSelector.UseSelector ? levelSelector.Level : 0);
    }
    public void LoadLevel()
    {
        gameEngine.GameUtilities.WaitFor(2, () =>
        {
            currentLevel = currentLevel.LoadNextLevel();
            currentLevel.IsEndOfGame(gameEngine.GameEvents.RaisePlayerPassedAllLevels)
                        .IsNotEndOfGame(() => {
                            gameEngine.ScoreboardController.DisplayLevel(currentLevel.LevelNumber);
                            gameEngine.GamePlayManager.ShowStage(currentLevel.LevelNumber);
                            gameEngine.GameUtilities.WaitFor(3, () => gameObjectBuilder.BuildLevel(currentLevel));
                        });
        });
    }

    public void JustLoadLevel()
    {
        currentLevel = currentLevel.LoadNextLevel();
        gameEngine.GamePlayManager.JustShowStage();
        gameObjectBuilder.BuildLevel(currentLevel);
    }

    public void HasNoMoreTanks(Action onNoMoreTanks) => currentLevel.Tanks.OnEmpty(onNoMoreTanks);
}
