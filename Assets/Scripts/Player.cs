using System;
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
        PickUper.Update(_inventory);
        _mover.Tick();
    }

    private void OnTriggerEnter(Collider other) { PickUper.OnTriggerEnter(other); }

    private void OnTriggerExit(Collider other) => PickUper.OnTriggerExit(other);

    //private void OnTriggerStay(Collider other) => PickUper.OnTriggerStay(other);
}