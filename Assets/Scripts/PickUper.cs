using System;
using System.Collections.Generic;
using ItemRelated;
using UnityEngine;

//Todo : make this a monobehaviour
public class PickUper
{
    public event Action<Item> ItemWithinReach;

    private readonly List<Item> _items = new List<Item>();
    private Item Item => _items[0];

    public void Update(Inventory inventory)
    {
        if (Item != null && PlayerInput.Instance.PickupButton)
            inventory.Pickup(Item);
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item == null)
            return;
        _items.Add(item);
        ItemWithinReach?.Invoke(item);
    }

    public void OnTriggerExit(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item == null) return;
        _items.Remove(item);
        ItemWithinReach?.Invoke(null);
    }
}