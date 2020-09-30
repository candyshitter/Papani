using ItemRelated;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace PlayTests
{
    public class equipment_tests
    {
        [Test]
        public void equip_sword_item_without_inventory()
        {
            var equipment = new GameObject("equipment").AddComponent<Equipment>();
            var item = inventory_helpers.GetItem();
            
            equipment.Equip(item);
            Assert.IsNotNull(equipment.GetItemOfItemType(item.SlotType));
        }

        [Test]
        public void cant_equip_if_slot_doesnt_exist()
        {
            var equipment = new GameObject("equipment").AddComponent<Equipment>();
            var item = Substitute.For<IItem>();
            item.SlotType.Returns(SlotType.NonEquippable);
            
            equipment.Equip(item);
            Assert.IsNull(equipment.GetItemOfItemType(item.SlotType));
        }
    }
}