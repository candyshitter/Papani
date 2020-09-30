using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class with_negative_mouse_x : player_input_test
    {
        [UnityTest]
        public IEnumerator turns_left()
        {
            yield return Helpers.LoadMovementTestsScene();

            var playerTransform = Helpers.GetPlayer().transform;

            PlayerInput.Instance.MouseX.Returns(-1);

            var originalRotation = playerTransform.rotation;
            yield return new WaitForSeconds(0.5f);

            var turnAmount = Helpers.CalculateTurn(originalRotation, playerTransform.rotation);
            
            Assert.Less(turnAmount, 0);
        }
        
    }
    public class with_positive_mouse_x : player_input_test
    {
        [UnityTest]
        public IEnumerator turns_right()
        {
            yield return Helpers.LoadMovementTestsScene();

            var player = Helpers.GetPlayer();

            PlayerInput.Instance.MouseX.Returns(1);

            var originalRotation = player.transform.rotation;
            yield return new WaitForSeconds(0.5f);

            var turnAmount = Helpers.CalculateTurn(originalRotation, player.transform.rotation);
            
            Assert.Greater(turnAmount, 0);
        }
        
    }
}