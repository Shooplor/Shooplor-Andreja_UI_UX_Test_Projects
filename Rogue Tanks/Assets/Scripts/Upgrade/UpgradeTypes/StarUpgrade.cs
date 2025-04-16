using UnityEngine;

public class StarUpgrade : IUpgradeType
{
    public void Upgrade(GameObject tank) =>
        tank.GetComponent<WeaponController>().UpgradeWeapon();
}
