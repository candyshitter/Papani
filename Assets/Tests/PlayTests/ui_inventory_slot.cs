using ItemRelated;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace PlayTests
{
    public class ui_inventory_slot
    {
        [Test]
        public void when_item_is_set_changes_icon_to_match()
        {
            var inventoryPanel = inventory_panel.GetUIInventoryPanel();
            var slot = inventoryPanel.UISlots[SlotType.Sword][0];
            var item = Substitute.For<IItem>();
            var sprite = Sprite.Create(Texture2D.redTexture, 
                new Rect(0, 0, 4, 4), Vector2.zero);
            item.Icon.Returns(sprite);
            
            
            slot.SetItem(item);

            Assert.AreSame(sprite, slot.Icon);
        }        
        [Test]
        public void when_item_is_set_image_is_enabled()
        {
            var inventoryPanel = inventory_panel.GetUIInventoryPanel();
            var slot = inventoryPanel.UISlots[SlotType.Sword][0];
            var item = Substitute.For<IItem>();
            var sprite = Sprite.Create(Texture2D.redTexture, 
                new Rect(0, 0, 4, 4), Vector2.zero);
            item.Icon.Returns(sprite);
            
            
            slot.SetItem(item);

            Assert.IsTrue(slot.ImageEnabled);
        }        
        [Test]
        public void when_item_is_not_set_image_is_disabled()
        {
            var inventoryPanel = inventory_panel.GetUIInventoryPanel();
            var slot = inventoryPanel.UISlots[SlotType.Sword][0];
            slot.SetItem(null);

            Assert.IsFalse(slot.ImageEnabled);
        }
    }
}