using System;
using ItemRelated;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PlayTests
{
    public static class inventory_helpers
    {
        public static UIInventoryPanel GetInventoryPanelWithItems(int numberOfItems)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UIInventoryPanel>("Assets/Prefabs/UI/Inventory Panels.prefab");
            var panel = Object.Instantiate(prefab);
            var inventory = GetInventory(numberOfItems);
            panel.BindToInventory(inventory);
            return panel;
        }

        public static Inventory GetInventory(int numberOfItems = 0)
        {
            var inventory =  new GameObject("Inventory").AddComponent<Inventory>();
            for (var i = 0; i < numberOfItems; i++)
            {
                var item = GetItem();
                inventory.Pickup(item);
            }

            return inventory;

        }

        public static Item GetItem()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Item>("Assets/Prefabs/Items/Test Items/Right Hand Test Item.prefab");
            return Object.Instantiate(prefab);
        }

        public static UISelectionCursor GetSelectionCursor()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<UISelectionCursor>("Assets/Prefabs/UI/Selection Cursor.prefab");
            return Object.Instantiate(prefab);
            
        }

        public static Item GetSwordItem()
        {
            throw new NotImplementedException();
        }

        public static Item GetShieldItem()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Item>("Assets/Prefabs/Items/Test Items/Left Hand shield.prefab");
            return Object.Instantiate(prefab);
        }

        public static Item GetSpearItem()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Item>("Assets/Prefabs/Items/Test Items/Both Hands Spear.prefab");
            return Object.Instantiate(prefab);
        }
    }

}