using System.Collections;
using a_player;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class game_state_machine
    {
        [SetUp]
        public void setup()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
        }
        
        [TearDown]
        public void teardown()
        {
            GameObject.Destroy(Object.FindObjectOfType<GameStateMachine>());
        }
        
        [UnityTest]
        public IEnumerator switches_to_loading_when_level_to_load_selected()
        {
            yield return Helpers.LoadAScene("Loader");
            var gameStateMachine = Object.FindObjectOfType<GameStateMachine>();
            
            Assert.AreEqual(typeof(Menu), gameStateMachine.CurrentStateType);
            
            PlayButton.LevelToLoad = "Level 1";
            yield return null;
            
            Assert.AreEqual(typeof(LoadLevel), gameStateMachine.CurrentStateType);
        }
        [UnityTest]
        public IEnumerator switches_to_play_when_level_to_load_completed()
        {
            yield return Helpers.LoadAScene("Loader");
            var gameStateMachine = Object.FindObjectOfType<GameStateMachine>();
            
            Assert.AreEqual(typeof(Menu), gameStateMachine.CurrentStateType);
            
            PlayButton.LevelToLoad = "Level 1";
            yield return null;
            
            Assert.AreEqual(typeof(LoadLevel), gameStateMachine.CurrentStateType);

            yield return new WaitUntil(() => gameStateMachine.CurrentStateType == typeof(Play));
            
            Assert.AreEqual(typeof(Play), gameStateMachine.CurrentStateType);
        }
        [UnityTest]
        public IEnumerator switches_from_play_to_pause_when_pause_button_pressed()
        {
            yield return Helpers.LoadAScene("Loader");
            var gameStateMachine = GameObject.FindObjectOfType<GameStateMachine>();
            
            PlayButton.LevelToLoad = "Level 1";
            
            yield return new WaitUntil(() => gameStateMachine.CurrentStateType == typeof(Play));
            
            //Hit pause
            PlayerInput.Instance.PausePress.Returns(true);
            yield return null;

            Assert.AreEqual(typeof(Pause), gameStateMachine.CurrentStateType);
        }
        [UnityTest]
        public IEnumerator switches_from_play_to_inventory_pause_when_inventory_pause_button_pressed()
        {
            yield return Helpers.LoadAScene("Loader");
            var gameStateMachine = GameObject.FindObjectOfType<GameStateMachine>();
            
            PlayButton.LevelToLoad = "Level 1";
            
            yield return new WaitUntil(() => gameStateMachine.CurrentStateType == typeof(Play));

            PlayerInput.Instance.InventoryButtonPress.Returns(true);
            yield return null;

            Assert.AreEqual(typeof(InventoryPause), gameStateMachine.CurrentStateType);
        }
        [UnityTest]
        public IEnumerator only_allows_one_instance_to_exist()
        {
            var firstStateMachine = new GameObject("First State Machine").AddComponent<GameStateMachine>();
            var secondStateMachine = new GameObject("Second State Machine").AddComponent<GameStateMachine>();
            yield return null;
            
            Assert.IsTrue(secondStateMachine == null); //IsNull doesn't work for some reason
            Assert.IsNotNull(firstStateMachine);
        }
    }
}