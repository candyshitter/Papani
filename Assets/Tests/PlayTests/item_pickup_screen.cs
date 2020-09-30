using NUnit.Framework;
using PlayTests;

namespace items_tests
{
    public class item_pickup_screen
    {

        [Test]
        public void picking_up_item_for_the_first_time_brings_up_screen()
        {
            var inventory = inventory_helpers.GetInventory();
            var item = inventory_helpers.GetItem();
            Assert.IsFalse(inventory.HasPickedUpItem(item));
            inventory.Pickup(item);
            Assert.IsTrue(inventory.HasPickedUpItem(item));
        }
        [Test]
        public void picking_up_item_for_the_second_time_does_not_bring_up_screen()
        {
            var inventory = inventory_helpers.GetInventory();
            var item = inventory_helpers.GetItem();
            var secondItem = inventory_helpers.GetItem();
            Assert.IsFalse(inventory.HasPickedUpItem(item));
            inventory.Pickup(item);
            Assert.IsTrue(inventory.HasPickedUpItem(item));
            Assert.IsTrue(inventory.HasPickedUpItem(secondItem));
        }
    }
}