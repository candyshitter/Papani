using System;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour, ITakeHits
    {
        [SerializeField] private int _maxHealth = 5;
        public event Action OnDied;
        public int Health { get; private set; }
        
        private void OnEnable() => Health = _maxHealth;
        public void TakeHit(int amount)
        {
            Health -= amount;
            if (Health <= 0)
                Die();
        }
        private void Die()
        {
            OnDied?.Invoke();
            Debug.Log("Died");
        }

        [ContextMenu("Take Lethal Damage")]private void TakeLethalDamage() => TakeHit(Health);

    }
}