using Entities;
using UnityEngine;

public class Dead : IState
{
    private float _despawnTime;
    private readonly Entity _entity;

    public Dead(Entity entity)
    {
        _entity = entity;
    }

    public void Tick()
    {
        if (Time.time>= _despawnTime)
            GameObject.Destroy(_entity.gameObject);
    }

    public void OnEnter()
    {
        //Drop loot, decay
        _despawnTime = Time.time + 5;
    }

    public void OnExit()
    {
        
    }
}