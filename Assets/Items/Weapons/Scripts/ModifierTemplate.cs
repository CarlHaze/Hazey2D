using UnityEngine;

namespace Items.Weapons
{
    [CreateAssetMenu(fileName = "New Modifier", menuName = "Game/Items/Weapons/Modifier")]
    public class ModifierTemplate : ScriptableObject
    {
        public string modifierName;
        public ModifierType type;
        public float value;
        [TextArea(3, 5)]
        public string description;
    
        public enum ModifierType
        {
            DamageMultiplier,
            AttackSpeedMultiplier,
            RangeMultiplier,
            CritChanceFlat,
            CritMultiplierFlat
        }
    
        public float ApplyModifier(float baseValue)
        {
            switch (type)
            {
                case ModifierType.DamageMultiplier:
                case ModifierType.AttackSpeedMultiplier:
                case ModifierType.RangeMultiplier:
                    return baseValue * (1 + value);
                case ModifierType.CritChanceFlat:
                case ModifierType.CritMultiplierFlat:
                    return baseValue + value;
                default:
                    return baseValue;
            }
        }
    }
}