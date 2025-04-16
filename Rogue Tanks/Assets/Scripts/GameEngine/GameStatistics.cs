using System;
using System.Collections.Generic;

public static class GameStatistics
{
    public static int HiScore = 20000;
    public static int StageNumber = 1;

    private static Dictionary<int, int> pointSystem = new Dictionary<int, int>();
    private static Dictionary<int, int> DestroyedTanks = new Dictionary<int, int>();
    static GameStatistics()
    {
        pointSystem.Add(6, 100);
        pointSystem.Add(7, 200);
        pointSystem.Add(8, 300);
        pointSystem.Add(9, 400);
    }
    public static Tuple<int,int> GetEnemyInfo(int tankType)
    {
        if (DestroyedTanks.ContainsKey(tankType))
            return new Tuple<int, int>(DestroyedTanks[tankType], pointSystem[tankType]);
        else return new Tuple<int, int>(0, 0);
    }
    public static void AddStatistics(Dictionary<int, int> destroyedTanks) => DestroyedTanks = destroyedTanks;

    public static void ForEachStat(Action<int, int, int> onStat) =>
        DestroyedTanks.ForEach(keyValuePair=>onStat(keyValuePair.Key, keyValuePair.Value, pointSystem[keyValuePair.Key]));
}
