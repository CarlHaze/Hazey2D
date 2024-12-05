using UnityEngine;

namespace Items.Weapons
{
    [System.Serializable]
    public class BaseWeaponStats
    {
        public float damage;
        public float attackSpeed;
        public float range;
        public float criticalChance;
        public float criticalMultiplier;
    }

    public enum WeaponRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythical
    }
}