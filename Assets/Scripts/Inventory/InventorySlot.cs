using System;

namespace ItemRelated
{
    public class InventorySlot
    {
        public SlotType SlotType;
        public Item Item { get; private set; }
        public bool IsEmpty => Item == null;

        public event Action ItemChanged;
    
        public InventorySlot(SlotType slotType) => SlotType = slotType;

        public void SetItem(Item item)
        {
            Item = item;
            ItemChanged?.Invoke();
        }
    }
}