using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Entity))]
    public class EntityAnimator : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Die = Animator.StringToHash("Die");

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            GetComponent<Entity>().OnDied += () => _animator.SetBool(Die, true);
        }
    }
}
