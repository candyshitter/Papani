using UnityEngine;

namespace ItemRelated
{
    //Move all logic related to picking up items here
    //You should be able to have equipment without inventory
    public class Equipment : MonoBehaviour
    {
        //[SerializeField] private EquipmentHolder _shieldHolder;
        //[SerializeField] private EquipmentHolder _swordHolder;
        public EquipmentDictionary EquippedInventory = new EquipmentDictionary
        {
            {SlotType.Sword, new EquipmentHolder()}, 
            {SlotType.Shield, new EquipmentHolder()}, 
            {SlotType.Bow, new EquipmentHolder()}, 
            {SlotType.Gauntlet, new EquipmentHolder()}, 
            {SlotType.Armor, new EquipmentHolder()}, 
        };
        
        public void Equip(IItem item)
        {
            var equippingItem = item as Item;
            if (item == null || !EquippedInventory.ContainsKey(item.SlotType)) return;
            EquippedInventory[item.SlotType].item = equippingItem;
        }

        public Item GetItemOfItemType(SlotType itemSlotType) => 
            !EquippedInventory.ContainsKey(itemSlotType) ? null : EquippedInventory[itemSlotType].item;
    }
}