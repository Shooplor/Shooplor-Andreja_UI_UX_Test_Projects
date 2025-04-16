using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileStatEffect
{
    public string Tile;
    public List<StatEffect> Effects;
}

public class TileStatEffects : MonoBehaviour
{
    private static TileStatEffects _instance;

    public static TileStatEffects Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError($"[Error] Instance of TileStatEffects does not exist!");
            }
            return _instance;
        }
    }

    public List<TileStatEffect> StatEffects;

    void Awake()
    {
        _instance = this;
    }

    public List<StatEffect> GetStatEffectsForTile(string name)
    {
        var tile = StatEffects.Find(x => x.Tile == name);
        return tile != null ? tile.Effects : null;
    }
}
