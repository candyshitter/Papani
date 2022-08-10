using System;
using ItemRelated;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot :
    MonoBehaviour, 
    IPointerDownHandler, 
    IDragHandler, 
    IEndDragHandler, 
    IPointerEnterHandler, 
    IPointerExitHandler
{
    public event Action<UIInventorySlot> OnSlotClicked; 
    
    [SerializeField] private Image _image;
    [SerializeField] private Image _selectedImage;
    [SerializeField] private Image _focusedImage;
    [SerializeField] private SlotType _slotType;
    [SerializeField] private bool _isEquipmentSlot;
    private InventorySlot _inventorySlot;

    public IItem Item { get; private set; }

    public bool IsEmpty => Item == null;
    public Sprite Icon => _image.sprite;
    public bool ImageEnabled => _image.enabled;
    public SlotType SlotType => _slotType;
    public bool IsEquipmentSlot => _isEquipmentSlot;

    public virtual void SetItem(IItem item)
    {
        Item = item;
        _image.sprite = Item != null ? item.Icon : null;
        _image.enabled = item != null;
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        OnSlotClicked?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var slot = eventData.pointerCurrentRaycast.gameObject?.GetComponentInParent<UIInventorySlot>();
        if (slot != null)
            slot.OnPointerDown(eventData);
        else
            OnPointerDown(eventData);
    }
    
    public void OnDrag(PointerEventData eventData) {}
    public void BecomeSelected()
    {
        if (_selectedImage)
            _selectedImage.enabled = true;
    }  
    public void BecomeDeselected()
    {
        if (_selectedImage)
            _selectedImage.enabled = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_focusedImage)
            _focusedImage.enabled = true;    
    }

    public void OnPointerExit(PointerEventData eventData) => DisableFocusedImage();

    private void OnDisable() => DisableFocusedImage();

    private void DisableFocusedImage()
    {
        if (_focusedImage)
            _focusedImage.enabled = false;
    }

    public void Bind(InventorySlot inventorySlot)
    {
        if (_inventorySlot != null)
            _inventorySlot.ItemChanged -= HandleItemChanged;
        _inventorySlot = inventorySlot;
        inventorySlot.ItemChanged += HandleItemChanged;
        HandleItemChanged();
    }

    private void HandleItemChanged() => SetItem(_inventorySlot.Item);
}