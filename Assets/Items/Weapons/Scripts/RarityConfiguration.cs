using UnityEngine;
using System.Collections.Generic;

namespace Items.Weapons
{
    [CreateAssetMenu(fileName = "New Rarity Config", menuName = "Game/Items/Weapons/Rarity Configuration")]
    public class RarityConfiguration : ScriptableObject
    {
        [System.Serializable]
        public class RaritySettings
        {
            public WeaponRarity rarity;
            public Color rarityColor;
            [Range(0, 1)]
            public float dropChance;
            public Vector2Int modifierCountRange = new Vector2Int(0, 2);
            public float statMultiplier = 1f;
        }

        public List<RaritySettings> raritySettings;

        public RaritySettings GetSettingsForRarity(WeaponRarity rarity)
        {
            return raritySettings.Find(x => x.rarity == rarity);
        }
    }
}