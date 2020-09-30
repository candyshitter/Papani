using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class a_moving_cube
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator moving_forward_changes_position()
        {
            //Arrange
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = Vector3.zero;
            
            for (int i = 0; i < 10; i++)
            {
                //Act
                cube.transform.position += Vector3.forward;
                yield return null; //(Was) waitFor(0.5f)
                
                //Assert
                Assert.AreEqual(i + 1,cube.transform.position.z);
            }
            
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
