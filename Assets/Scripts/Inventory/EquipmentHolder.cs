using System;
using UnityEngine;

namespace ItemRelated
{
    [Serializable]
    public class EquipmentHolder
    {
        [SerializeField] private Transform itemHolder;
        public Transform ItemHolder => itemHolder;
        public Item item;
    }
}