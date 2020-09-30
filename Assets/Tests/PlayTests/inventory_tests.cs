using ItemRelated;
using NUnit.Framework;
using UnityEngine;

namespace PlayTests
{
    public class inventory_tests
    {
        //Add items
        [Test]
        public void can_add_items()
        {
            var inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            var item = inventory_helpers.GetItem();
            
            inventory.Pickup(item);

            Assert.AreEqual(1, inventory.Items.Count);
        }
        //Place into HotbarSlot
        [Test]
        public void can_move_item_into_specific_slot()
        {
            var inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            var item = inventory_helpers.GetItem();
            var slotType = item.SlotType;
            inventory.Pickup(item, 6);
            
            Assert.AreEqual(item, inventory.GetItemInSlot(6, slotType));
        }
        
        //Change Slots / Move
        [Test]
        public void can_move_item_to_empty_slot()
        {
            var inventory = new GameObject("INVENTORY").AddComponent<Inventory>();
            var item = inventory_helpers.GetItem();
            inventory.Pickup(item);
            var slotType = item.SlotType;

            Assert.AreEqual(item, inventory.GetItemInSlot(7, slotType));

            
            inventory.Move(slotType,7, 8);
            
            Assert.AreEqual(item, inventory.GetItemInSlot(8, slotType));
        }
        
        //Remove Items
        //Drop Items
        //Hotkey / Hotbar assignment
        //Change visuals
        //Modify Stats
        //Persist & Load
    }
}