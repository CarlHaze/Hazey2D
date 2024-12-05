// CharacterStats.cs
using UnityEngine;
using System;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private StatTemplate statTemplate;
    private Dictionary<StatType, Stat> stats = new();

    private void Awake()
    {
        InitializeStats();
    }

    private void InitializeStats()
    {
        if (statTemplate == null) return;

        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            float baseValue = statTemplate.GetBaseValue(statType);
            stats[statType] = new Stat(baseValue);
        }
    }

    public float GetStatValue(StatType type)
    {
        return stats.TryGetValue(type, out Stat stat) ? stat.CurrentValue : 0f;
    }

    public void AddModifier(StatType type, StatModifier modifier, bool isPermanent = false)
    {
        if (stats.TryGetValue(type, out Stat stat))
        {
            stat.AddModifier(modifier, isPermanent);
        }
    }

    public void RemoveModifier(StatType type, StatModifier modifier)
    {
        if (stats.TryGetValue(type, out Stat stat))
        {
            stat.RemoveModifier(modifier);
        }
    }

    public void ClearTemporaryModifiers()
    {
        foreach (var stat in stats.Values)
        {
            stat.ClearTemporaryModifiers();
        }
    }
}