using UnityEngine;
using System.Collections.Generic;

namespace Items.Weapons
{
    public class Weapon : MonoBehaviour
{
    private WeaponTemplate template;
    private List<ModifierTemplate> modifiers = new List<ModifierTemplate>();
    private WeaponRarity rarity;
    private BaseWeaponStats currentStats;

    public void Initialize(WeaponTemplate template, WeaponRarity rarity, RarityConfiguration rarityConfig)
    {
        this.template = template;
        this.rarity = rarity;
        
        // Clone base stats
        currentStats = new BaseWeaponStats
        {
            damage = template.baseStats.damage,
            attackSpeed = template.baseStats.attackSpeed,
            range = template.baseStats.range,
            criticalChance = template.baseStats.criticalChance,
            criticalMultiplier = template.baseStats.criticalMultiplier
        };
        
        // Apply rarity multiplier
        var settings = rarityConfig.GetSettingsForRarity(rarity);
        currentStats.damage *= settings.statMultiplier;
    }

    public void AddModifier(ModifierTemplate modifier)
    {
        modifiers.Add(modifier);
        RecalculateStats();
    }

    private void RecalculateStats()
    {
        // Reset to base stats
        currentStats = new BaseWeaponStats
        {
            damage = template.baseStats.damage,
            attackSpeed = template.baseStats.attackSpeed,
            range = template.baseStats.range,
            criticalChance = template.baseStats.criticalChance,
            criticalMultiplier = template.baseStats.criticalMultiplier
        };

        // Apply all modifiers
        foreach (var modifier in modifiers)
        {
            switch (modifier.type)
            {
                case ModifierTemplate.ModifierType.DamageMultiplier:
                    currentStats.damage = modifier.ApplyModifier(currentStats.damage);
                    break;
                case ModifierTemplate.ModifierType.AttackSpeedMultiplier:
                    currentStats.attackSpeed = modifier.ApplyModifier(currentStats.attackSpeed);
                    break;
                // Add other cases as needed
            }
        }
    }
}
}