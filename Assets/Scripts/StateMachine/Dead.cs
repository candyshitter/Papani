using Entities;
using UnityEngine;

public class Dead : IState
{
    private float _despawnTime;
    private readonly Entity _entity;
    private readonly EntityStateMachine _esm;

    public Dead(Entity entity, EntityStateMachine entityStateMachine)
    {
        _entity = entity;
        _esm = entityStateMachine;
    }

    private void Tick()
    {
        if (Time.time>= _despawnTime)
            Object.Destroy(_entity.gameObject);
    }

    public void OnEnter()
    {
        _despawnTime = Time.time + 5;
        _esm.OnUpdate += Tick;
    }

    public void OnExit()
    {
        _esm.OnUpdate -= Tick;
    }
}