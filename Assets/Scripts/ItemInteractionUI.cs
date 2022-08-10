using ItemRelated;
using UnityEngine;

public class ItemInteractionUI : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<Player>().PickUper.ItemWithinReach += OnItemWithinReach;
        gameObject.SetActive(false);
    }

    private void OnItemWithinReach(Item item)
    {
        gameObject.SetActive(item != null);
    }
}
