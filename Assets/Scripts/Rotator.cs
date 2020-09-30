using UnityEngine;

public class Rotator
{
    private readonly Camera _camera;
    private readonly Transform _transform;

    public Rotator(Player player)
    {
        _camera = Object.FindObjectOfType<Camera>();
        _transform = player.transform;
    }

    public void Tick()
    {
        _transform.rotation = Quaternion.Euler(_camera.transform.forward);;
    }
}