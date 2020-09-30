using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemRelated
{
    public class Inventory : MonoBehaviour
    {
        private const int DEFAULT_INVENTORY_SIZE = 25;
    
        public event Action<Item> ItemPickedUp;
        public event Action<Item> ItemUnequipped;
        public event Action<Item> ItemEquipped;

        public readonly Dictionary<SlotType, List<InventorySlot>> Slots = new Dictionary<SlotType, List<InventorySlot>>
        {
            {SlotType.NonEquippable, new List<InventorySlot>()},
            {SlotType.Armor, new List<InventorySlot>()},
            {SlotType.Sword, new List<InventorySlot>()},
            {SlotType.Shield, new List<InventorySlot>()},
        };
    
        private Transform _itemRoot;

        private readonly Dictionary<ItemObject, bool> _itemsPickedUp = 
            new Dictionary<ItemObject, bool>();
        
        public IEnumerable<InventorySlot> AllInventorySlots => Slots.Values.SelectMany(slots => slots);

        public List<Item> Items
        {
            get
            {
                var items = new List<Item>();
                foreach (var list in Slots.Values.ToList())
                    items.AddRange(from inventorySlot in list where !inventorySlot.IsEmpty select inventorySlot.Item);

                return items;
            }
        }


        private void Awake()
        {
            AddProperInventorySlotAmount();
            CreateItemRoot();
            ItemPickedUp += OnItemPickedUp;
        }

        private void CreateItemRoot()
        {
            _itemRoot = new GameObject("Items").transform;
            _itemRoot.transform.SetParent(transform);
        }

        private void OnItemPickedUp(Item item)
        {
            if (HasPickedUpItem(item)) return;
            _itemsPickedUp[item.ItemObject] = true;
        }

        public void Pickup(Item item, int? slotInt = null)
        {
            var slot = !slotInt.HasValue ? 
                FindFirstAvailableSlot(item.SlotType) : 
                GetSlotAt(item.SlotType, slotInt);
            if (slot == null || item == null) return;
        
            PutInInventorySlot(item, slot);
        }

        private InventorySlot GetSlotAt(SlotType itemSlotType, int? slotInt)
            => !slotInt.HasValue ? null : Slots[itemSlotType][slotInt.Value]; 

        private void PutInInventorySlot(Item item, InventorySlot slot)
        {
            slot?.SetItem(item);
            PutItemInRoot(item, false);

            ItemPickedUp?.Invoke(item);
        }

        private void PutItemInRoot(Item item, bool setActive)
        {
            item.transform.SetParent(_itemRoot);
            item.gameObject.SetActive(setActive);
            item.WasPickedUp = true;
        }

    
        //If the item is moved to an equipment slot, equip 
        //If the item is moved from an eqipment slot, unequip

        public void Move(SlotType slotType, int fromSlot, int toSlot)
        {
            var toItem = Slots[slotType][toSlot].Item;
            var fromItem = Slots[slotType][fromSlot].Item;

            Slots[slotType][toSlot].SetItem(fromItem);
            Slots[slotType][fromSlot].SetItem(toItem);
        
        }

        public bool HasPickedUpItem(Item item) =>
            _itemsPickedUp.ContainsKey(item.ItemObject)
            && _itemsPickedUp[item.ItemObject];

        public InventorySlot FindFirstAvailableSlot(SlotType itemSlotType) => 
            Slots[itemSlotType].FirstOrDefault(s => s.IsEmpty);

        private void AddProperInventorySlotAmount()
        {
            AddSlotsToList(SlotType.NonEquippable, DEFAULT_INVENTORY_SIZE);
            AddSlotsToList(SlotType.Armor, DEFAULT_INVENTORY_SIZE);
            AddSlotsToList(SlotType.Shield, 10);
            AddSlotsToList(SlotType.Sword, 10);
        }

        private void AddSlotsToList(SlotType slotType, int length)
        {
            var slots = Slots[slotType];
            for (var i = 0; i < length; i++)
                slots.Add(new InventorySlot(slotType));
        }

        public Item GetItemInSlot(int slot, SlotType slotType)
            => Slots[slotType][slot].Item;
    }
}