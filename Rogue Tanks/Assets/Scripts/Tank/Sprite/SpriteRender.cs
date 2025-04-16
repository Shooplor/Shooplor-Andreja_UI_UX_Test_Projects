using UnityEngine;

public class SpriteRender : MonoBehaviour
{
    public Sprite PlayerTank;
    public Sprite EnemyTank1;
    public Sprite EnemyTank2;
    public Sprite EnemyTank3;
    public Sprite EnemyTank4;

    public void CreatePlayerTank() => this.gameObject.GetComponent<SpriteRenderer>().sprite = PlayerTank;
    public void CreateEnemyTank1() => this.gameObject.GetComponent<SpriteRenderer>().sprite = EnemyTank1;
    public void CreateEnemyTank2() => this.gameObject.GetComponent<SpriteRenderer>().sprite = EnemyTank2;
    public void CreateEnemyTank3() => this.gameObject.GetComponent<SpriteRenderer>().sprite = EnemyTank3;
    public void CreateEnemyTank4() => this.gameObject.GetComponent<SpriteRenderer>().sprite = EnemyTank4;
}
