using UnityEngine;

public class ItemInteractionUI : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<Player>().PickUper.ItemWithinReach += OnItemWithinReach;
        gameObject.SetActive(false);
    }

    private void OnItemWithinReach(bool turnOn)
    {
        gameObject.SetActive(turnOn);
    }
}
