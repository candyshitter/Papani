using System.Collections;
using a_player;
using Entities;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace state_machine
{
    public class entity_state_machine
    {
        [UnityTest]
        public IEnumerator start_in_idle_state()
        {
            yield return Helpers.LoadEntityStateMachineTestsScene();
            var stateMacine = GameObject.FindObjectOfType<EntityStateMachine>();
            Assert.AreEqual(typeof(Idle), stateMacine.CurrentStateType);
        }

        [UnityTest]
        public IEnumerator switches_to_chase_player_when_in_range()
        {
            yield return Helpers.LoadEntityStateMachineTestsScene();

            var player = Helpers.GetPlayer().transform;

            var stateMachine = GameObject.FindObjectOfType<EntityStateMachine>();

            player.position = stateMachine.transform.position + Vector3.right * 5.9f;
            yield return null;
            Assert.AreEqual(typeof(Idle), stateMachine.CurrentStateType);

            player.position = stateMachine.transform.position + Vector3.right * 2f;
            Debug.Log(EntityStateMachine.DistanceFlat(player.position, stateMachine.transform.position));
            yield return null;
            Assert.AreEqual(typeof(ChasePlayer), stateMachine.CurrentStateType);
        }

        [UnityTest]
        public IEnumerator switches_to_dead_once_health_reaches_zero()
        {
            yield return Helpers.LoadEntityStateMachineTestsScene();

            var stateMacine = GameObject.FindObjectOfType<EntityStateMachine>();
            var entity = stateMacine.GetComponent<Entity>();

            yield return null;
            Assert.AreEqual(typeof(Idle), stateMacine.CurrentStateType);

            entity.TakeHit(entity.Health - 1);
            yield return null;
            Assert.AreEqual(typeof(Idle), stateMacine.CurrentStateType);

            entity.TakeHit(entity.Health);
            yield return null;
            Assert.AreEqual(typeof(Dead), stateMacine.CurrentStateType);
        }
        
    }
}