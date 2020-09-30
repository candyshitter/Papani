using Sirenix.OdinInspector;
using UnityEngine;

namespace ItemRelated
{
    [CreateAssetMenu(menuName = "Item", fileName = "Item Object")]
    [InlineEditor]
    public class ItemObject : ScriptableObject
    {
        [SerializeField] private SlotType slotType;
        public SlotType SlotType => slotType;


        [SerializeField, PreviewField(100, ObjectFieldAlignment.Left)]
        private Sprite _icon;
        public Sprite Icon => _icon;

        [SerializeField, Space(20)] private StatMod[] _statMods;
        public StatMod[] StatMods => _statMods;
    }
}