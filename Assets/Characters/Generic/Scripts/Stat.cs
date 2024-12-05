// Stat.cs
using UnityEngine;
using System.Collections.Generic;

public class Stat
{
    private float baseValue;
    private readonly List<StatModifier> permanentModifiers = new();
    private readonly List<StatModifier> temporaryModifiers = new();

    public float CurrentValue
    {
        get
        {
            float final = baseValue;
            float percentAdd = 0;

            // Apply permanent modifiers
            foreach (var mod in permanentModifiers)
            {
                if (mod.Type == ModifierType.Flat)
                    final += mod.Value;
                else
                    percentAdd += mod.Value;
            }

            // Apply temporary modifiers
            foreach (var mod in temporaryModifiers)
            {
                if (mod.Type == ModifierType.Flat)
                    final += mod.Value;
                else
                    percentAdd += mod.Value;
            }

            // Apply percentage modifications
            final *= (1 + percentAdd);
            return final;
        }
    }

    public Stat(float baseValue)
    {
        this.baseValue = baseValue;
    }

    public void AddModifier(StatModifier modifier, bool isPermanent = false)
    {
        var list = isPermanent ? permanentModifiers : temporaryModifiers;
        list.Add(modifier);
    }

    public void RemoveModifier(StatModifier modifier)
    {
        permanentModifiers.Remove(modifier);
        temporaryModifiers.Remove(modifier);
    }

    public void ClearTemporaryModifiers()
    {
        temporaryModifiers.Clear();
    }
}