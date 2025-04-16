using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private IWeaponType weaponType;
    private WeaponUpgrades weaponUpgrades;

    public void Initialize(GameEngine gameEngine, SpriteController spriteController, Tank tank) =>
        weaponUpgrades = new WeaponUpgrades()
            .AddUpgradeLevel(() => weaponType.Bullets.ForEach(bullet => bullet.GetComponent<BulletMovement>().Speed = 45.0f))
            .AddUpgradeLevel(() => weaponType = new DoubleBulletFiringKeyboardWeapon(gameEngine, spriteController, tank, 45f))
            .AddUpgradeLevel(() => weaponType.Bullets.ForEach(bullet=>bullet.GetComponent<BulletController>().IncreaseBulletStrength()));

    public void SetWeaponType(IWeaponType weaponType)
    {
        this.weaponType = weaponType;
        weaponUpgrades.Reset();
    }

    public IInputActionController movementController = new NoActions();

    private void Update() => movementController.Fire(()=>weaponType.CanFire(() => weaponType.Fire()));

    public void UpgradeWeapon() => weaponUpgrades.Upgrade();
}
