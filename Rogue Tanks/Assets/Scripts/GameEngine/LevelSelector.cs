using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    private static LevelSelector instance;
    public static LevelSelector Instance
    {
        get
        {
            return instance;
        }
    }

    public int Level;
    public bool UseSelector;
    private int maxLevel = 0;

    void Awake()
    {
        instance = this;
        maxLevel = Resources.LoadAll("").Length;
        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseLevel()
    {
        Level++;
        if(Level >= maxLevel)
        {
            Level = 0;
        }
    }

    public void DecreaseLevel()
    {
        Level--;
        if(Level < 0)
        {
            Level = maxLevel - 1;
        }
    }
}
