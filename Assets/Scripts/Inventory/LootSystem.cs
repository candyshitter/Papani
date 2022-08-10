using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ItemRelated
{
    public class LootSystem : MonoBehaviour
    {
        [SerializeField] private AssetReference _lootItemHolderPrefab;
        private static LootSystem _lootSystemInstance;
        private const float DROP_RADIUS = 4;
        private static readonly Queue<LootItemHolder> LootItemHolders = new Queue<LootItemHolder>();

        private void Awake()
        {
            if (_lootSystemInstance != null)
                Destroy(gameObject);
            else
                _lootSystemInstance = this;
        }

        public static void Drop(Item item, Transform droppingTransform)
        {
            if (LootItemHolders.Any())
            {
                var lootItemHolder = LootItemHolders.Dequeue();
                lootItemHolder.gameObject.SetActive(true);
                AssignItemToHolder(lootItemHolder, item, droppingTransform);
            }
            else
                _lootSystemInstance.StartCoroutine(DropAsync(item, droppingTransform));
        }

        private static IEnumerator DropAsync(Item item, Transform droppingTransform)
        {
            var operation = _lootSystemInstance._lootItemHolderPrefab.InstantiateAsync();
            yield return operation;
        
            var lootItemHolder = operation.Result.GetComponent<LootItemHolder>();
            AssignItemToHolder(lootItemHolder, item, droppingTransform);
        }

        private static void AssignItemToHolder(LootItemHolder lootItemHolder, Item item, Transform droppingTransform)
        {
            lootItemHolder.TakeItem(item);
            var randomCirclePoint = Random.insideUnitCircle * DROP_RADIUS;
            var randomPosition = droppingTransform.position +
                                 new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);

            lootItemHolder.transform.position = randomPosition;
        }

        public static void AddToPool(LootItemHolder lootItemHolder)
        {
            lootItemHolder.gameObject.SetActive(false);
            LootItemHolders.Enqueue(lootItemHolder);
        }
    }
}