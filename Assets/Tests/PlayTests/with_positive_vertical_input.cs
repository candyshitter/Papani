using System.Collections;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_player
{
    public class player_input_test
    {
        [SetUp]
        public void setup()
        {
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
        }
    }

    public class with_positive_vertical_input : player_input_test
    {
        [UnityTest]
        public IEnumerator moves_forward()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            PlayerInput.Instance.Vertical.Returns(1);
            PlayerInput.Instance.Horizontal.Returns(1);

            float startPosZ = player.transform.position.z;

            yield return new WaitForSeconds(3);

            float endPosZ = player.transform.position.z;

            Assert.Greater(endPosZ, startPosZ);
        }
    }

    public class with_negative_vertical_input : player_input_test
    {
        [UnityTest]
        public IEnumerator moves_backwards()
        {
            yield return Helpers.LoadMovementTestsScene();
            var player = Helpers.GetPlayer();

            PlayerInput.Instance.Vertical.Returns(-1);
            PlayerInput.Instance.Horizontal.Returns(0);

            float startPosZ = player.transform.position.z;

            yield return new WaitForSeconds(3);

            float endPosZ = player.transform.position.z;

            Assert.Less(endPosZ, startPosZ);
        }
    }
}
