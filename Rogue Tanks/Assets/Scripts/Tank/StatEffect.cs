using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Speed = 0
}

public enum StatModifierType
{
    Additive,
    Multiplicative
}

[Serializable]
public class StatEffect
{
    public string Tag;
    public StatType Type;
    public StatModifierType ModifierType;
    public float Value;

    public StatEffect() { }

    public StatEffect(StatEffect statEffect)
    {
        Tag = statEffect.Tag;
        Type = statEffect.Type;
        ModifierType = statEffect.ModifierType;
        Value = statEffect.Value;
    }

    public float CalculateValue(float target)
    {
        switch(ModifierType)
        {
            case StatModifierType.Additive:
                return target + Value;
            case StatModifierType.Multiplicative:
                return target + target * Value;
        }

        return target;
    }
}
