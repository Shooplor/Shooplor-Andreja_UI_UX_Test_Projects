using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tanks
{
    private readonly List<Tank> tanks;

    public event Action AllTanksDestroyed;
    private void OnAllTanksDestroyed() => AllTanksDestroyed?.Invoke();
    public Tanks() => tanks = new List<Tank>();

    public void Add(Tank tank) => tanks.Add(tank);
    public void Destroy(Tank tank)
    {
        GameObject.Destroy(tank.gameObject);
        tanks.Remove(tank);
        tanks.Where(tank => tank.IsEnemyTank()).IsEmpty(OnAllTanksDestroyed);
    }
    public void Destroy(Tank tank, Action onDestroy)
    {
        Destroy(tank);
        onDestroy();
    }
    public void DestroyAllEnemyTanks()
    {
        tanks.ForEach(tank => tank.IsEnemyTank(()=>GameObject.Destroy(tank.gameObject)));
        tanks.RemoveAll(tank => tank.IsEnemyTank());
        tanks.Where(tank => tank.IsEnemyTank()).IsEmpty(OnAllTanksDestroyed);
    }
    public IEnumerable<Tank> GetPlayerTanks() => tanks.Where(tank => tank.IsPlayerTank());
    public IEnumerable<Tank> GetEnemyTanks() => tanks.Where(tank => tank.IsEnemyTank());
    public bool All(Func<Tank, bool> predicate) => tanks.All(predicate);
    public void DisableAllEnemyTanks() => tanks.ForEach(tank => tank.IsEnemyTank(()=>tank.DisableTank()));
    public void EnableAllEnemyTanks() => tanks.ForEach(tank => tank.IsEnemyTank(()=>tank.EnableTank()));
}
