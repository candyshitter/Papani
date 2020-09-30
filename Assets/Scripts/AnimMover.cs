using UnityEngine;

public class AnimMover : IMover
{
    private Animator _animator;
    private Player _player;

    public AnimMover(Player player)
    {
        _player = player;
        _animator = player.GetComponent<Animator>();
    }

    public void Tick()
    {
        _animator.speed = 1 + _player.Stats.Get(StatType.MoveSpeed);
    }
}