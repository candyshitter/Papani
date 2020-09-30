using ItemRelated;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Inventory))]
    public class NPCLoot : MonoBehaviour
    {
        [SerializeField] private Item[] _itemPrefabs;
    
        private Inventory _inventory;
        private EntityStateMachine _entityStateMachine;

        private void Start()
        {
            _entityStateMachine = GetComponent<EntityStateMachine>();
            _entityStateMachine.OnEntityStateChanged += HandleStateChanged;
        
            _inventory = GetComponent<Inventory>();
            foreach (var itemPrefab in _itemPrefabs)
                _inventory.Pickup(Instantiate(itemPrefab));
        }

        private void HandleStateChanged(IState state)
        {
            Debug.Log($"HandleStateChanged {state.GetType()}");
            if (state is Dead)
                DropLoot();
        }

        private void DropLoot()
        {
            foreach (var inventorySlots in _inventory.Slots.Values)
            {
                foreach(var slots in inventorySlots)
                    LootSystem.Drop(slots.Item, transform);
                inventorySlots.Clear();
            }
        }
    }
}