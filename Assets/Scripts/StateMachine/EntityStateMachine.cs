using System;
using Entities;
using UnityEngine;
using UnityEngine.AI;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private Entity _entity;

    public Type CurrentStateType => _stateMachine.CurrentState.GetType();
    public event Action OnUpdate;
    public event Action<IState> OnEntityStateChanged; 

    private void Awake()
    {
        var player = FindObjectOfType<Player>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _entity = GetComponent<Entity>();
        
        _stateMachine = new StateMachine();
        _stateMachine.OnStateChanged += state => OnEntityStateChanged?.Invoke(state);
        
        var idle = new Idle();
        var chasePlayer = new ChasePlayer(_navMeshAgent, player, this);
        var attack = new Attack();
        var dead = new Dead(_entity, this);

        _stateMachine.AddTransition(
            idle, 
            chasePlayer, 
            () => 
            DistanceFlat(
            _navMeshAgent.transform.position, 
            player.transform.position) < 5f);
                
        _stateMachine.AddTransition(
            chasePlayer, 
            attack, 
            () => 
            DistanceFlat(_navMeshAgent.transform.position, player.transform.position) < 2f);
        _stateMachine.AddAnyTransition(dead, () => _entity.Health <= 0);
        _stateMachine.SetState(idle);
    }

    public static float DistanceFlat(Vector3 source, Vector3 destination) =>
        Vector3.Distance(
            new Vector3(source.x, 0, source.z), 
            new Vector3(destination.x, 0, destination.z));

    private void Update()
    {
        _stateMachine.UpdateStates();
        OnUpdate?.Invoke();
    }
}