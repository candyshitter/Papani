/*using NUnit.Framework;
using PlayTests;

namespace items_tests
{
    public class active_item_change
    {
        [Test]
        public void active_right_hand_fills_right_hand()
        {
            var inventory = inventory_helpers.GetInventory(1);
            Assert.IsNotNull(inventory.SwordHolder);
        }
        [Test]
        public void active_left_hand_fills_left_hand()
        {
            var inventory = inventory_helpers.GetInventory();
            var item = inventory_helpers.GetShieldItem();
            inventory.Pickup(item);
            Assert.IsNotNull(inventory.ShieldHolder);
        }
        [Test]
        public void active_both_hands_fill_both_hands()
        {
            var inventory = inventory_helpers.GetInventory();
            var item = inventory_helpers.GetSpearItem();
            inventory.Pickup(item);
            Assert.IsNotNull(inventory.ShieldHolder.IsEmpty);
            Assert.IsNotNull(inventory.SwordHolder.IsEmpty);
        }

    }
}*/