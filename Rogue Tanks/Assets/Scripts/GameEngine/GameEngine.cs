using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public LevelController LevelController { get; private set; }
    public ScoreboardController ScoreboardController { get; private set; }
    public GameFactory GameFactory { get; private set; }
    public GameUtilities GameUtilities { get; private set; }
    public GameEvents GameEvents { get; private set; }
    public UpgradeBuilder UpgradeBuilder { get; private set; }
    public GameTiles GameTiles { get; private set; }
    public GamePlayManager GamePlayManager { get; private set; }
    public ActiveGameObjectsController ActiveGameObjectsController { get; private set; }
    private void Awake()
    {
        GameTiles = gameObject.GetComponent<GameTiles>();
        GameFactory = gameObject.GetComponent<GameFactory>();
        GameUtilities = gameObject.GetComponent<GameUtilities>();
        GamePlayManager = gameObject.GetComponent<GamePlayManager>();

        ActiveGameObjectsController = new ActiveGameObjectsController(this);
        UpgradeBuilder = new UpgradeBuilder(this);
        LevelController = new LevelController(this);
        ScoreboardController = new ScoreboardController(this);

        GameEvents = new GameEvents();
        GameEvents.TankBaseDestroyed += LoadGameOverScene;
        GameEvents.PlayerTankDestroyed += LoadGameOverScene;
        GameEvents.EnemyTanksDestroyed += LevelController.LoadLevel;
        GameEvents.PlayerPassedAllLevels += LoadGameOverScene;
    }

    private void Start() => LevelController.JustLoadLevel();

    private void LoadGameOverScene()
    {
        GamePlayManager.ShowGameOver();
        GameUtilities.WaitFor(4, SceneManager.LoadGameOverScene);
    }
}
