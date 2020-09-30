using UnityEngine;

public interface IPlayerInput
{
    float Horizontal { get; }
    float Vertical { get; }
    float MouseX { get; }
    bool PausePress { get; }
    Vector2 MousePosition { get; }
    bool InventoryButtonPress { get;}
    bool Jump { get; }
    bool PickupButton { get; }
}

