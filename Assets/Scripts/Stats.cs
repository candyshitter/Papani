using System.Collections.Generic;
using ItemRelated;

public class Stats
{
    private readonly Dictionary<StatType, float> _stats = new Dictionary<StatType, float>();
    public void Add(StatType statType, float value)
    {
        if (_stats.ContainsKey(statType))
            _stats[statType] += value;
        else
            _stats[statType] = value;
    }

    public float Get(StatType statType)
    {
        if (_stats.ContainsKey(statType))
            return _stats[statType];
        _stats.Add(statType, 0);
        return _stats[statType];
    }

    public void Remove(StatType statType, float value)
    {
        if (_stats.ContainsKey(statType))
            _stats[statType] -= value;

    }

    public void Bind(Inventory inventory)
    {
        inventory.ItemEquipped += HandleItemEquipped;
        inventory.ItemUnequipped += HandleItemUnequipped;
    }

    private void HandleItemUnequipped(Item item)
    {
        if (item == null) return;
        foreach (var statMod in item.StatMods)
        {
            Remove(statMod.StatType, statMod.Value);
        }
    }

    private void HandleItemEquipped(Item item)
    {
        if (item == null) return;
        foreach (var statMod in item.StatMods)
        {
            Add(statMod.StatType, statMod.Value);
        }
    }
}

public enum StatType
{
    MoveSpeed
}