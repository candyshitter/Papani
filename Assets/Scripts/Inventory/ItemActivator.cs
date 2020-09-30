using UnityEngine;

namespace ItemRelated
{
    public abstract class ItemActivator : MonoBehaviour 
    {
        public bool CanUse => Time.time >= _nextUseTime;
        protected float _nextUseTime;

        public abstract void Use();
    }
}