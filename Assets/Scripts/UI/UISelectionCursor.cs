﻿using UnityEngine;
using UnityEngine.UI;

public class UISelectionCursor : MonoBehaviour
{
    private UIInventoryPanel _inventoryPanel;
    [SerializeField] private Image _image;
    public bool IconVisible => _image != null && _image.sprite != null && _image.enabled;
    public Sprite Icon => _image.sprite;
    

    private void Awake()
    {
        _inventoryPanel = FindObjectOfType<UIInventoryPanel>();
        _image.enabled = false;
    }

    private void OnEnable() => _inventoryPanel.OnSelectionChanged += HandleSelectionChanged;
    private void OnDisable() => _inventoryPanel.OnSelectionChanged -= HandleSelectionChanged;

    private void HandleSelectionChanged()
    {
        _image.sprite = _inventoryPanel.Selected != null ? _inventoryPanel.Selected.Icon : null;
        _image.enabled = _image.sprite != null;
    }

    private void Update()
    {
        transform.position = PlayerInput.Instance.MousePosition;
    }
}