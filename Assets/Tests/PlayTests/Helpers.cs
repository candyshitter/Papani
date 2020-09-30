using System.Collections;
using NSubstitute;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace a_player
{
    public static class Helpers
    {
        public static IEnumerator LoadMovementTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("MovementTests");
            while (!operation.isDone)
                yield return null;
            
        }        
        public static IEnumerator LoadItemsTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("ItemTests");
            while (!operation.isDone)
                yield return null;
            operation = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            while (!operation.isDone)
                yield return null;
            
        }
        public static IEnumerator LoadEntityStateMachineTestsScene()
        {
            var operation = SceneManager.LoadSceneAsync("EntityStateMachineTests");
            while (!operation.isDone)
                yield return null;
        }
        public static IEnumerator LoadMenuScene()
        {
            var operation = SceneManager.LoadSceneAsync("Loader");
            while (!operation.isDone)
                yield return null;
        }        
        public static IEnumerator LoadAScene(string sceneName)
        { 
            var operation = SceneManager.LoadSceneAsync(sceneName);
            while (!operation.isDone)
                yield return null;
        }
        public static Player GetPlayer()
        {
            var player = Object.FindObjectOfType<Player>();
            PlayerInput.Instance = Substitute.For<IPlayerInput>();
            return player;
        }
        
        public static float CalculateTurn(Quaternion originalRot, Quaternion transformRot)
        {
            var cross = Vector3.Cross(originalRot * Vector3.forward, transformRot * Vector3.forward);
            var dot = Vector3.Dot(cross, Vector3.up);
            return dot;
        }


    }
}