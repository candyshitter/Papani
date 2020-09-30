using Entities;
using UnityEngine;

namespace ItemRelated
{
    public class ItemRaycaster : ItemActivator
    {
        private float _range = float.PositiveInfinity;
    
        private RaycastHit[] _results = new RaycastHit[10]; 
        private int _layermask;
        [SerializeField] private int _damage = 1;

        private void Awake() => _layermask = LayerMask.GetMask("Default");
        public override void Use()
        {
            var ray  = UnityEngine.Camera.main.ViewportPointToRay(Vector3.one/2f); 
        
            var hits = Physics.RaycastNonAlloc(ray, _results, _range, _layermask, QueryTriggerInteraction.Collide);
        
            var nearest = new RaycastHit();
            var nearestDistance = double.MaxValue;
            for (var i = 0; i < hits; i++)
            {
                var distance = Vector3.Distance(transform.position, _results[i].point);
                if (!(distance < nearestDistance)) continue;
                nearest = _results[i];
                nearestDistance = distance;
            }

            if (nearest.transform == null) return;
            var takeHits = nearest.collider.GetComponent<ITakeHits>();
            takeHits?.TakeHit(_damage);

        }
    }
}