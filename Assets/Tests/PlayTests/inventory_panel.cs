using System.Collections;
using System.Linq;
using ItemRelated;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayTests
{
    public class inventory_panel
    {
        [UnityTearDown, UnitySetUp]
        public IEnumerator tear_down()
        {
            var inventory = Object.FindObjectOfType<Inventory>();
            var inventoryPanel = Object.FindObjectOfType<UIInventoryPanel>();
            if (inventory != null)
                Object.Destroy(inventory.gameObject);            
            if (inventoryPanel != null)
                Object.Destroy(inventoryPanel.gameObject);
            yield return null;
        }

        [Test]
        public void has_equal_amount_UISlots_and_InventorySlots()
        {
            var uiInventoryPanel = GetUIInventoryPanel();
            var inventory = GetInventory();
            Assert.AreEqual(inventory.AllInventorySlots.ToArray().Length, 
                uiInventoryPanel.AllUIInventorySlots.ToArray().Length);
        }

        [Test]
        public void bound_to_empty_inventory_has_all_slots_empty()
        {
            var inventoryPanel = GetUIInventoryPanel();
            var inventory = GetInventory();

            inventoryPanel.BindToInventory(inventory);
            foreach (var slot in inventoryPanel.AllUIInventorySlots)
            {
                Assert.IsTrue(slot.IsEmpty);
            }
        }

        [UnityTest]
        public IEnumerator bound_to_inventory_fills_slot_for_each_item
        ([NUnit.Framework.Range(0, 25)]int numberOfItems)
        {
            var inventory = GetInventory(numberOfItems);
            var uiInventoryPanel = GetUIInventoryPanel();
            foreach (var slot in uiInventoryPanel.AllUIInventorySlots)
                Assert.IsTrue(slot.IsEmpty);
            
            uiInventoryPanel.BindToInventory(inventory);

            var uiInventorySlots = uiInventoryPanel.AllUIInventorySlots.ToArray();
            for (var i = 0; i < uiInventorySlots.Length; i++)
            {
                var shouldBeEmpty = i >= numberOfItems;
                Assert.AreEqual(shouldBeEmpty, uiInventorySlots[i].IsEmpty);
            }
            
            Object.Destroy(inventory.gameObject);
            Object.Destroy(uiInventoryPanel.gameObject);
            yield return null;
        }



        [Test]
        public void bound_to_null_inventory_has_empty_slots()
        {
            var uiInventoryPanel = GetUIInventoryPanel();
            uiInventoryPanel.BindToInventory(null);
            foreach (var uiSlot in uiInventoryPanel.AllUIInventorySlots)
            {
                Assert.IsTrue(uiSlot.IsEmpty);
            }
        }
        [Test]
        public void bound_to_valid_inventory_then_to_null_inventory_has_empty_slots()
        {
            var uiInventoryPanel = GetUIInventoryPanel();
            var inventory = GetInventory(1);
            
            uiInventoryPanel.BindToInventory(inventory);
            uiInventoryPanel.BindToInventory(null);
            foreach (var uiSlot in uiInventoryPanel.AllUIInventorySlots)
                Assert.IsTrue(uiSlot.IsEmpty);
        }

        [Test]
        public void updates_slots_when_items_are_moved()
        {
            var inventoryPanel = GetUIInventoryPanel();
            var inventory = GetInventory(1);
            
            inventoryPanel.BindToInventory(inventory);
            var slotType = SlotType.NonEquippable;
            
            inventory.Move(slotType, 0,4);
            Assert.AreSame(inventory.GetItemInSlot(4, slotType), inventoryPanel.UISlots[slotType][4].Item);
        }

        [Test]
        public void picking_up_different_item_types_puts_item_in_different_categories()
        {
            var inventory = GetInventory();
            var item = GetItem();
            inventory.Pickup(item);
        }

        private Item GetItem() => inventory_helpers.GetItem();

        private Inventory GetInventory(int numberOfItems = 0) => inventory_helpers.GetInventory(numberOfItems);

        public static UIInventoryPanel GetUIInventoryPanel()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UIInventoryPanel>("Assets/Prefabs/UI/Inventory Panels.prefab");
            return Object.Instantiate(prefab);
        }
    }
}
