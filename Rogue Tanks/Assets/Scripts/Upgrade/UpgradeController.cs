using System;
using UnityEngine;

public class UpgradeController
{
    private SpriteRenderer spriteRenderer;
    private readonly GameEngine gameEngine;
    private Coroutine upgradeCoroutine;
    private Upgrade upgrade;

    public UpgradeController(GameEngine gameEngine)
    {
        this.gameEngine = gameEngine;
    }
    public void DisplayUpgrade(Upgrade upgrade)
    {
        spriteRenderer = upgrade.GetComponentInChildren<SpriteRenderer>();
        DestroyCurrentUpgrade();
        SetNewUpgrade(upgrade);
        upgradeCoroutine = gameEngine.GameUtilities.InfiniteRepeat(1, second => second.Equals(10, DestroyCurrentUpgrade), new Action[] { Show,Hide}); 
    }

    private void Hide() => spriteRenderer.gameObject.SetActive(false);
    private void Show() => spriteRenderer.gameObject.SetActive(true);

    private void SetNewUpgrade(Upgrade upgrade)
    {
        upgrade.UpgradeCollected += DestroyCurrentUpgrade;
        this.upgrade = upgrade;
    }

    private void DestroyCurrentUpgrade()
    {
        if (upgrade == null) return;
        gameEngine.GameUtilities.StopCoroutine(upgradeCoroutine);
        GameObject.Destroy(this.upgrade.gameObject);
    }
}
