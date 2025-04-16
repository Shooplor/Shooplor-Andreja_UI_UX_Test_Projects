using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverProcessor : MonoBehaviour
{
    public Text HighScore;
    public Text Stage;
    public Text Player1HighScore;

    public Text Tank6Points;
    public Text Tank6KilledEnemies;
    public Text Tank7Points;
    public Text Tank7KilledEnemies;
    public Text Tank8Points;
    public Text Tank8KilledEnemies;
    public Text Tank9Points;
    public Text Tank9KilledEnemies;

    public Text TotalTanksKilled;

    int totalTanksKilled = 0;
    void Start()
    {
        HighScore.text = GameStatistics.HiScore.ToString();
        Stage.text = $"STAGE   {GameStatistics.StageNumber}";
        int totalPlayerPoints = 0;
        GameStatistics.ForEachStat((tankType, killedEnemies, pointsPerEnemy) =>
        {
            totalPlayerPoints += (killedEnemies * pointsPerEnemy);
            totalTanksKilled += killedEnemies;
        });
        Player1HighScore.text = totalPlayerPoints.ToString();
        StartCoroutine(Count6Enemies());
    }

    IEnumerator Count6Enemies()
    {
        yield return new WaitForSeconds(1);
        Tank6Points.text = $"0     PTS";
        var enemyInfo = GameStatistics.GetEnemyInfo(6);
        var killedEnemies = enemyInfo.Item1;
        var points = enemyInfo.Item2;
        for (int i = 1; i <= killedEnemies; i++)
        {
            Tank6Points.text = $"{i * points}     PTS";
            Tank6KilledEnemies.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Count7Enemies());
    }

    IEnumerator Count7Enemies()
    {
        Tank7Points.text = $"0     PTS";
        var enemyInfo = GameStatistics.GetEnemyInfo(7);
        var killedEnemies = enemyInfo.Item1;
        var points = enemyInfo.Item2;
        for (int i = 1; i <= killedEnemies; i++)
        {
            Tank7Points.text = $"{i * points}     PTS";
            Tank7KilledEnemies.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Count8Enemies());
    }

    IEnumerator Count8Enemies()
    {
        Tank8Points.text = $"0     PTS";
        var enemyInfo = GameStatistics.GetEnemyInfo(8);
        var killedEnemies = enemyInfo.Item1;
        var points = enemyInfo.Item2;
        for (int i = 1; i <= killedEnemies; i++)
        {
            Tank8Points.text = $"{i * points}     PTS";
            Tank8KilledEnemies.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Count9Enemies());
    }

    IEnumerator Count9Enemies()
    {
        Tank9Points.text = $"0     PTS";
        var enemyInfo = GameStatistics.GetEnemyInfo(9);
        var killedEnemies = enemyInfo.Item1;
        var points = enemyInfo.Item2;
        for (int i = 1; i <= killedEnemies; i++)
        {
            Tank9Points.text = $"{i * points}     PTS";
            Tank9KilledEnemies.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.5f);
        ShowTotal();
    }

    void ShowTotal()
    {
        TotalTanksKilled.text = totalTanksKilled.ToString();
    }
}
