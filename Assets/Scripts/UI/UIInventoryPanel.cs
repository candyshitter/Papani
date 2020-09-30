using System;
using System.Collections.Generic;
using System.Linq;
using ItemRelated;
using TMPro;
using UnityEngine;

public class UIInventoryPanel : MonoBehaviour
{
    public event Action OnSelectionChanged;
    private Inventory _inventory;
    public UIInventorySlot Selected { get; private set; }

    public Dictionary<SlotType, List<UIInventorySlot>> UISlots = new Dictionary<SlotType, List<UIInventorySlot>>
    {
        {SlotType.NonEquippable, new List<UIInventorySlot>()},
        {SlotType.Armor, new List<UIInventorySlot>()},
        {SlotType.Sword, new List<UIInventorySlot>()},
        {SlotType.Shield, new List<UIInventorySlot>()},
    };

    public IEnumerable<UIInventorySlot> AllUIInventorySlots => UISlots.Values.SelectMany(uiSlots => uiSlots);

    private void Awake()
    {
        var uiInventorySlots = GetComponentsInChildren<UIInventorySlot>();
        foreach (var uiSlot in uiInventorySlots)
        {
            UISlots[uiSlot.SlotType].Add(uiSlot);
        }
            //.OrderBy(t => t.IsEquipmentSlot == false).ToArray();
        RegisterSlotsForClickCallbacks();
    }

    private void RegisterSlotsForClickCallbacks()
    {
        foreach (var slot in UISlots.Values.SelectMany(list => list))
            slot.OnSlotClicked += HandleSlotClicked;
    }

    private void HandleSlotClicked(UIInventorySlot slot)
    {
        if (Selected != null && SlotCanHoldItem(slot, Selected.Item))
        {
            Swap(slot);
            Selected.BecomeDeselected();
            Selected = null;
        }
        else if(!slot.IsEmpty)
        {
            Selected = slot;
            Selected.BecomeSelected();
        }

        OnSelectionChanged?.Invoke();
    }

    private bool SlotCanHoldItem(UIInventorySlot slot, IItem selectedItem) =>
        slot.SlotType == selectedItem.SlotType;

    private void Swap(UIInventorySlot slot) => 
        _inventory.Move(slot.SlotType, GetSlotIndex(Selected), GetSlotIndex(slot));

    private int GetSlotIndex(UIInventorySlot slot)
    {
        for (var i = 0; i < UISlots[slot.SlotType].Count; i++)
            if (UISlots[slot.SlotType][i] == slot)
                return i;
        return -1;
    }

    public void BindToInventory(Inventory inventory)
    {
        _inventory = inventory;
        if (_inventory != null)
            BindNewInventory(inventory);
        else ClearSlots();
    }
    
    private void BindNewInventory(Inventory inventory)
    {
        foreach (var uiSlots in UISlots.Values)
        {
            for (var i = 0; i < uiSlots.Count; i++)
            {
                var uiSlot = uiSlots[i];
                uiSlot.Bind(inventory.Slots[uiSlot.SlotType][i]);
                uiSlot.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
            }
        }

        // for (var i = 0; i < UISlots.Length; i++)
        // {
        //     var uiSlot = UISlots[i];
        //     var inventorySlot = inventory.FindFirstAvailableSlot(uiSlot.SlotType); //returns the same slot every time
        //     uiSlot.Bind(inventorySlot);
        //     uiSlot.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
        // }
    }

    private void ClearSlots()
    {
        foreach (var slot in UISlots.Values.SelectMany(slots => slots))
            slot.SetItem(null);
    }
 
}