using System;

public class GameEvents
{
    public event Action TankBaseDestroyed;
    public void RaiseOnTankBaseDestroyed() => TankBaseDestroyed?.Invoke();
    public event Action PlayerTankDestroyed;
    public void RaiseOnPlayerTankDestroyed() => PlayerTankDestroyed?.Invoke();
    public event Action EnemyTanksDestroyed;
    public void RaiseOnEnemyTanksDestroyed() => EnemyTanksDestroyed?.Invoke();
    public event Action PlayerPassedAllLevels;
    public void RaisePlayerPassedAllLevels() => PlayerPassedAllLevels?.Invoke();
}
