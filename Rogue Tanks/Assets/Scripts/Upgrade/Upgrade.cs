using System;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public event Action UpgradeCollected;
    private IUpgradeType upgradeType;

    public Upgrade Initialize(IUpgradeType upgradeType, Sprite sprite)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        this.upgradeType = upgradeType;
        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision) =>
        collision.gameObject.GetComponent<Tank>()
        .CanGetUpgrade(() => CollectUpgrade(collision.gameObject));

    private void CollectUpgrade(GameObject gameObject)
    {
        upgradeType.Upgrade(gameObject);
        UpgradeCollected?.Invoke();
    }

    public void SetVisible(bool isVisible) => gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(isVisible);
}
