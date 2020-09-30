using System;
using ItemRelated;
using UnityEngine;

public class PickUper
{
    public event Action<bool> ItemWithinReach;
    public Item Item;
    
    //When close to object, show menu
    //When not close, not show
    //When having picked up item, do not show
    //When having picked up item, but another item is
    //    still in range, continue showing

    public void OnTriggerStay(Collider other)
    {   
        if(Item == null)
            Item = other.GetComponent<Item>();
        if (Item == null || Item.WasPickedUp) return;
        ItemWithinReach?.Invoke(true);
    }

    public void DeselectItem()
    {
        Item = null;
        ItemWithinReach?.Invoke(false);
    }
}