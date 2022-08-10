using UnityEngine.AI;

public class ChasePlayer : IState
{
    private readonly NavMeshAgent _navMeshAgent;
    private readonly Player _player;
    private readonly EntityStateMachine _esm;

    public ChasePlayer(NavMeshAgent navMeshAgent, Player player, EntityStateMachine entityStateMachine)
    {
        _navMeshAgent = navMeshAgent;
        _player = player;
        _esm = entityStateMachine;
    }

    private void Tick()
    {
        _navMeshAgent.SetDestination(_player.transform.position);
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _esm.OnUpdate += Tick;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        _esm.OnUpdate -= Tick;
    }
}