using ItemRelated;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IMover _mover;
    private Rotator _rotator;
    private Inventory _inventory;
    public Stats Stats { get; private set; }

    public PickUper PickUper { get; private set; }

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
        
        Stats = new Stats();
        Stats.Bind(_inventory);
        
        _mover = new AnimMover(this);
        
        PickUper = new PickUper();
    }
    
    private void Update()
    {
        if (Pause.Active) return;
        if(PickUper.Item != null && PlayerInput.Instance.PickupButton)
        {
            _inventory.Pickup(PickUper.Item);
            PickUper.DeselectItem();
        }
        _mover.Tick();
    }

    private void OnTriggerExit(Collider other) => PickUper.DeselectItem();

    private void OnTriggerStay(Collider other) => PickUper.OnTriggerStay(other);
}