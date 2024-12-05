using UnityEngine;
using System;

namespace Items.Weapons
{
    [CreateAssetMenu(fileName = "New Weapon Template", menuName = "Game/Items/Weapons/Weapon Template")]
    public class WeaponTemplate : ScriptableObject
    {
        public string weaponName;
        public Sprite weaponSprite;
        public GameObject weaponPrefab;
        public BaseWeaponStats baseStats;
        public WeaponCategory weaponCategory;
        [SerializeField] private int subcategoryIndex;

        // Main categories
        public enum WeaponCategory
        {
            Twohanded,
            Onehanded,
            Ranged,
            Ammo,
            Unarmed,
            Shield,
            Throwing
        }
        
        // Subcategories
        public enum TwohandedType
        {
            Battleaxe,
            Greatsword,
            Greatmaul,
            Trident,
            Staff,
            Scythe
        }
        
        public enum OnehandedType
        {
            Waraxe,
            Mace,
            Longsword,
            Warhammer,
            Wand,
            Flail,
            Shortstaff,
            Dagger
        }
        
        public enum RangedType
        {
            Shortbow,
            Longbow,
            Crossbow,
            Slingshot
        }
        
        public enum AmmoType
        {
            Arrow,
            Bolt,
            Stone
        }
        
        public enum UnarmedType
        {
            Fist,
            Claw,
            Gauntlet
        }
        
        public enum ShieldType
        {
            Buckler,
            Kiteshield,
            Towershield
        }
        
        public enum ThrowingType
        {
            ThrowingKnife,
            ThrowingAxe,
            Javelin,
            Boomerang
        }

        // Property to get/set the correct subcategory
        public object Subcategory
        {
            get
            {
                return weaponCategory switch
                {
                    WeaponCategory.Twohanded => (TwohandedType)subcategoryIndex,
                    WeaponCategory.Onehanded => (OnehandedType)subcategoryIndex,
                    WeaponCategory.Ranged => (RangedType)subcategoryIndex,
                    WeaponCategory.Ammo => (AmmoType)subcategoryIndex,
                    WeaponCategory.Unarmed => (UnarmedType)subcategoryIndex,
                    WeaponCategory.Shield => (ShieldType)subcategoryIndex,
                    WeaponCategory.Throwing => (ThrowingType)subcategoryIndex,
                    _ => TwohandedType.Battleaxe
                };
            }
            set
            {
                if (value is Enum enumValue)
                {
                    subcategoryIndex = Convert.ToInt32(enumValue);
                }
            }
        }

        // Helper method to get subcategory enum type
        public Type GetSubcategoryEnumType()
        {
            return weaponCategory switch
            {
                WeaponCategory.Twohanded => typeof(TwohandedType),
                WeaponCategory.Onehanded => typeof(OnehandedType),
                WeaponCategory.Ranged => typeof(RangedType),
                WeaponCategory.Ammo => typeof(AmmoType),
                WeaponCategory.Unarmed => typeof(UnarmedType),
                WeaponCategory.Shield => typeof(ShieldType),
                WeaponCategory.Throwing => typeof(ThrowingType),
                _ => typeof(TwohandedType)
            };
        }
    }
}