using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace ItemRelated
{
    [RequireComponent(typeof(Collider)), HideMonoScript]
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField, VerticalGroup("ItemObject")] 
            private ItemObject _item;
            
        [SerializeField, Space(20)] 
            private List<UseAction> _actions = new List<UseAction>();
        public bool WasPickedUp { get; set; }
        public event Action OnPickedUp;
        private void OnValidate() => GetComponent<Collider>().isTrigger = true;

        #region ButtonsAndAccessers

        [Button, HideIf("$_item", null), VerticalGroup("ItemObject")]
        public void CreateNewItemObject()
        {
            var asset = ScriptableObject.CreateInstance<ItemObject>();
            AssetDatabase.CreateAsset(asset, $"Assets/Scriptables/ItemObjects/{gameObject.name} Item Object.asset");
            AssetDatabase.SaveAssets();
            _item = asset;
        }

        [Button("Assign Actions Automatically")]
        private void AssignActions()
        {
            var activators = GetComponents<ItemActivator>();
            _actions.Clear();
            foreach (var activator in activators)
                _actions.Add(new UseAction {TargetActivator = activator});
        }

        #endregion

        #region ItemObjectAccessors

        public ItemObject ItemObject => _item;

        public Sprite Icon => _item.Icon;

        public StatMod[] StatMods => _item.StatMods;

        public SlotType SlotType => _item.SlotType;

        #endregion
        
    }


    public interface IItem
    {
        Sprite Icon { get; }
        SlotType SlotType { get; }
    }
}