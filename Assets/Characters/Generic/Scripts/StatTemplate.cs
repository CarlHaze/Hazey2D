// StatTemplate.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatTemplate", menuName = "Game/Stats/Stat Template")]
public class StatTemplate : ScriptableObject
{
    [System.Serializable]
    public class BaseStat
    {
        public StatType type;
        public float baseValue;
    }

    public BaseStat[] baseStats;

    // Helper method to get base value
    public float GetBaseValue(StatType type)
    {
        foreach (var stat in baseStats)
        {
            if (stat.type == type) return stat.baseValue;
        }
        return 0f;
    }
}