using System.Collections;
using ItemRelated;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class moving_into_an_item
    {
        private Player player;
        private Item item;

        [UnitySetUp]
        public IEnumerator init()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            yield return Helpers.LoadItemsTestsScene();
            player = Helpers.GetPlayer();
            PlayerInput.Instance.Vertical.Returns(1f);
            
            item = Object.FindObjectOfType<Item>();
        }
        [UnityTest]
        public IEnumerator makes_item_interaction_ui_appear()
        {
            var itemUI = Resources.FindObjectsOfTypeAll<ItemInteractionUI>()[0].gameObject;
            Assert.IsFalse(itemUI.activeSelf);
            item.transform.position = player.transform.position;
        
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(itemUI.activeSelf);        
        }
    
       
        /*[UnityTest]
        public IEnumerator changes_slot_1_icon_to_match_item_icon()
        {
            var inventoryPanel = Resources.FindObjectsOfTypeAll<UIInventoryPanel>()[0]; //Find disabled object
            var slotOne = inventoryPanel.GetComponentInChildren<UIInventorySlot>();
            
            Assert.AreNotSame(item.Icon, slotOne.Icon);
            
            item.transform.position = player.transform.position;
            PlayerInput.Instance.PickupButton.Returns(true);
            yield return null;
            yield return new WaitForFixedUpdate();

            Assert.AreEqual(item.Icon, slotOne.Icon);
        }*/
    }

}
