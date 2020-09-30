using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class base_state_machine
    {
        // A Test behaves as an ordinary method
        [Test]
        public void initial_set_state_switches_to_state()
        {
            var stateMachine = new StateMachine();
            IState firstState = Substitute.For<IState>();
            stateMachine.SetState(firstState);
            
            Assert.AreSame(firstState, stateMachine.CurrentState);
        }

        [Test]
        public void subsequent_set_state_switches_to_state()
        {
            var stateMachine = new StateMachine();
            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            
            stateMachine.SetState(secondState);
            Assert.AreSame(secondState, stateMachine.CurrentState);
        }
        [Test]
        public void transition_switches_state_when_condition_is_met()
        {
            var stateMachine = new StateMachine();
            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();
            stateMachine.AddTransition(firstState, secondState, () => true);
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            
            stateMachine.UpdateStates();
            
            Assert.AreSame(secondState, stateMachine.CurrentState);
        }
        [Test]
        public void transition_does_not_switch_state_when_condition_is_not_met()
        {
            var stateMachine = new StateMachine();
            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();
            stateMachine.AddTransition(firstState, secondState, () => false);
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            
            stateMachine.UpdateStates();
            
            Assert.AreSame(firstState, stateMachine.CurrentState);
        }
        [Test]
        public void transition_does_not_switch_state_when_not_in_correct_source_state()
        {
            var stateMachine = new StateMachine();
            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();
            IState thirdState = Substitute.For<IState>();
            
            stateMachine.AddTransition(secondState, thirdState, () => true);
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            
            stateMachine.UpdateStates();
            
            Assert.AreSame(firstState, stateMachine.CurrentState);
        }
        [Test]
        public void transition_from_any_switches_state_when_condition_is_met()
        {
            var stateMachine = new StateMachine();
            IState firstState = Substitute.For<IState>();
            IState secondState = Substitute.For<IState>();

            stateMachine.AddAnyTransition(secondState, () => true);
            
            stateMachine.SetState(firstState);
            Assert.AreSame(firstState, stateMachine.CurrentState);
            
            stateMachine.UpdateStates();
            
            Assert.AreSame(secondState, stateMachine.CurrentState);
        }
    }
}
