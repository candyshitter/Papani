using UnityEngine;

namespace ItemRelated
{
    public class LootItemHolder : MonoBehaviour
    {
        [SerializeField] private Transform _itemTransformHolder;
        [SerializeField] private float _rotationSpeed = 1f;

        public void TakeItem(Item item)
        {
            PutItemInHolder(item.transform);
            item.WasPickedUp = false;
            item.OnPickedUp += () => LootSystem.AddToPool(this);
        }

        private void PutItemInHolder(Transform itemTransform)
        {
            itemTransform.SetParent(_itemTransformHolder);
            itemTransform.localPosition = Vector3.zero;
            itemTransform.localRotation = Quaternion.identity;
            itemTransform.gameObject.SetActive(true);
        }

        private void Update()
        {
            var amount = Time.deltaTime * _rotationSpeed;
            _itemTransformHolder.Rotate(0, amount, 0);
        }
    }
}
