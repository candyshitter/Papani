using UnityEngine;

namespace ItemRelated
{
    public class ItemLogger : ItemActivator
    {
        public override void Use()
        {
            Debug.Log(transform.name + " was used");
        }
    }
}