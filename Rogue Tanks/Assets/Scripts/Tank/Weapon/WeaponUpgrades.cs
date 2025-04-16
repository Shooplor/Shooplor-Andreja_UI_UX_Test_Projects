using System;
using System.Collections.Generic;

public class WeaponUpgrades
{
    private int upgradeLevel = 0;
    private Dictionary<int, Action> upgrades;

    public WeaponUpgrades() =>
        upgrades = new Dictionary<int, Action>();

    public WeaponUpgrades AddUpgradeLevel(Action upgradeAction)
    {
        upgrades.Add(upgrades.Count + 1, upgradeAction);
        return this;
    }

    public void Upgrade()
    {
        upgradeLevel++;
        if (upgradeLevel > upgrades.Count) return;
        upgrades[upgradeLevel].Invoke();
    }

    public void Reset() => upgradeLevel = 0;
}
