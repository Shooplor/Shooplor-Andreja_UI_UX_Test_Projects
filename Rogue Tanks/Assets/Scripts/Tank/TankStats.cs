using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TankStats : MonoBehaviour
{
    public Tank Tank { get; private set; }
    private Dictionary<int, int> tankPoints = new Dictionary<int, int>();
    public Dictionary<int, int> TankPoints { get => tankPoints; }
    public int TankType { get; private set; }
    public int Points { get; private set; }
    public int Lives { get; private set; } = 1;

    public float LavaTimer = 2f;

    private int lavaTileCount = 0;
    private float lavaStartTime = float.MinValue;

    public int LavaTileCount
    {
        get => lavaTileCount;
        set
        {
            lavaTileCount = value;
            UpdateLava();
        }
    }

    public List<StatEffect> StatEffects = new List<StatEffect>();

    public void Initialize(int tankType, int lives)
    {
        this.TankType = tankType;
        this.Lives = lives;
        this.Tank = GetComponentInParent<Tank>();
    }

    void Update()
    {
        if(lavaStartTime >= 0 && Time.time - lavaStartTime > LavaTimer)
        {
            HitTank();
            ResetLava();
        }
    }

    public void HitTank()
    {
        Tank.BulletHit(null);
    }

    public void AddLive(int numberOfLives) => Lives += numberOfLives;

    public void AddLive(int numberOfLives, Action<int> onLiveAdd)
    {
        AddLive(numberOfLives);
        onLiveAdd(Lives);
    }

    public TankStats DecreaseLife(Action onDecreaseLife)
    {
        Lives -= 1;
        onDecreaseLife();
        return this;
    }

    public TankStats DecreaseLife(Action<int> onLiveDecreased)
    {
        Lives -= 1;
        onLiveDecreased(Lives);
        return this;
    }

    public TankStats TankStillHaveLivesLeft(Action onLivesLeft)
    {
        Lives.IsGreaterThanOrEqualTo(1, onLivesLeft);
        return this;
    }

    public TankStats TankHasNoMoreLivesLeft(Action onNoMoreLivesLeft)
    {
        Lives.Equals(0, onNoMoreLivesLeft);
        return this;
    }

    public void AddPoint(int destroyedTankType)=>
        tankPoints.AddValue(destroyedTankType, 1);

    public void AddStatEffect(StatEffect statEffect, bool stackable = false)
    {
        if(stackable || !StatEffects.Exists(x => x.Tag == statEffect.Tag))
        {
            StatEffects.Add(statEffect);
        }
    }

    public void RemoveStatEffect(string tag)
    {
        StatEffects.RemoveAll(x => x.Tag == tag);
    }

    public List<StatEffect> GetStatEffects(StatType type)
    {
        return StatEffects.Where(x => x.Type == type).ToList();
    }

    public void UpdateLava() => lavaStartTime = lavaTileCount > 0 && lavaStartTime < 0 ? Time.time : float.MinValue;
    public void ResetLava() => lavaStartTime = float.MinValue;

    public void IncrementLavaCounter() => LavaTileCount++;
    public void DecrementLavaCounter() => LavaTileCount--;
}