using UnityEngine;
using System.Collections.Generic;

namespace Items.Weapons
{
    [CreateAssetMenu(fileName = "Weapon Factory", menuName = "Game/Items/Weapons/Weapon Factory")]
    public class WeaponFactory : ScriptableObject
    {
        public RarityConfiguration rarityConfig;
        public List<WeaponTemplate> weaponTemplates;
        public List<ModifierTemplate> modifierTemplates;

        public Weapon CreateRandomWeapon()
        {
            // Select random template and rarity
            var template = weaponTemplates[Random.Range(0, weaponTemplates.Count)];
            var rarity = RollRarity();
        
            // Create weapon instance
            var weaponObj = Instantiate(template.weaponPrefab);
            var weapon = weaponObj.GetComponent<Weapon>();
            if (weapon == null) weapon = weaponObj.AddComponent<Weapon>();
        
            // Initialize weapon
            weapon.Initialize(template, rarity, rarityConfig);
        
            // Add random modifiers based on rarity
            AddRandomModifiers(weapon, rarity);
        
            return weapon;
        }

        private WeaponRarity RollRarity()
        {
            float roll = Random.value;
            float currentChance = 0;
        
            foreach (var settings in rarityConfig.raritySettings)
            {
                currentChance += settings.dropChance;
                if (roll <= currentChance)
                    return settings.rarity;
            }
        
            return WeaponRarity.Common;
        }

        private void AddRandomModifiers(Weapon weapon, WeaponRarity rarity)
        {
            var settings = rarityConfig.GetSettingsForRarity(rarity);
            int modifierCount = Random.Range(settings.modifierCountRange.x, settings.modifierCountRange.y + 1);
        
            for (int i = 0; i < modifierCount; i++)
            {
                var randomModifier = modifierTemplates[Random.Range(0, modifierTemplates.Count)];
                weapon.AddModifier(randomModifier);
            }
        }
    }
}