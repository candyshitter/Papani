using UnityEngine;

public class Mover : IMover
{
    private readonly Player _player;
    private readonly CharacterController _characterController;

    private const float SPEED = 6.0f;
    private const float JUMP_SPEED = 8.0f;
    private Vector3 _moveDirection = Vector3.zero;
    private Camera _camera;

    public Mover(Player player)
    {
        _player = player;
        _characterController = player.GetComponent<CharacterController>();
    }

    public void Tick()
    {
        if (_characterController.isGrounded)
        {
            _moveDirection = _player.transform.rotation
                             * new Vector3(PlayerInput.Instance.Horizontal, 0.0f,
                                 PlayerInput.Instance.Vertical);
            _moveDirection.Normalize();
            _moveDirection *= SPEED;
            if (PlayerInput.Instance.Jump)
                _moveDirection.y += JUMP_SPEED;
        }
        _characterController.SimpleMove(_moveDirection);
    }
    
}