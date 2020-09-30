using UnityEngine;

namespace ItemRelated
{
    [RequireComponent(typeof(Inventory))]
    public class InventoryUse : MonoBehaviour
    {
        private Inventory _inventory; //Has a reference to thee players inventory
        private void Awake() => _inventory = GetComponent<Inventory>();

        private void Update() => UseActionsIfPressed();

        private void UseActionsIfPressed()
        {

        }

        private bool WasPressed(UseMode useMode)
        {
            switch (useMode)
            {
                case UseMode.LeftClick: return Input.GetButtonDown("Fire1");
                case UseMode.RightClick: return Input.GetButtonDown("Fire2");
                case UseMode.Space: return Input.GetButtonDown("Jump");
            }

            return false;
        }
    }
}