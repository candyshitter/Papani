using System;
using System.Collections.Generic;
using ItemRelated;
using UnityEngine;

public abstract class UnitySerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    private readonly List<TKey> _keyData = new List<TKey>();
    private readonly List<TValue> _valueData = new List<TValue>();

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        Clear();
        for (var i = 0; i < _keyData.Count && i < _valueData.Count; i++) this[_keyData[i]] = _valueData[i];
    }

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        _keyData.Clear();
        _valueData.Clear();

        foreach (var item in this)
        {
            _keyData.Add(item.Key);
            _valueData.Add(item.Value);
        }
    }
}
[Serializable] public class EquipmentDictionary : UnitySerializedDictionary<SlotType, EquipmentHolder> {}