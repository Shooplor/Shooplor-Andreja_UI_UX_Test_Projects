public class ActiveGameObjectsController
{
    private readonly GameEngine gameEngine;
    public Tanks ActiveTanks = new Tanks();

    private UpgradeController upgradeController;
    private TankBase tankBase;
    public void SetTankBase(TankBase tankBase) => this.tankBase = tankBase;
    public TankBase TankBase { get => tankBase; }

    public ActiveGameObjectsController(GameEngine gameEngine)
    {
        upgradeController = new UpgradeController(gameEngine);
        ActiveTanks.AllTanksDestroyed += () => gameEngine.LevelController.HasNoMoreTanks(gameEngine.GameEvents.RaiseOnEnemyTanksDestroyed);
        this.gameEngine = gameEngine;
    }

    public void AddTank(Tank tank) => ActiveTanks.Add(tank);

    public void DestroyTank(Tank tank) => ActiveTanks.Destroy(tank, ()=> gameEngine.UpgradeBuilder.CreateRandomUpgrade(upgradeController.DisplayUpgrade));
}
