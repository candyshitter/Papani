using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public static IPlayerInput Instance { get; set; }
    public float Horizontal => Input.GetAxis("Horizontal");
    public float Vertical => Input.GetAxis("Vertical");
    public float MouseX => Input.GetAxis("Mouse X");
    public bool PausePress => Input.GetKeyDown(KeyCode.Escape);
    public Vector2 MousePosition => Input.mousePosition;
    public bool InventoryButtonPress => Input.GetKeyDown(KeyCode.Tab);
    public bool Jump => Input.GetKeyDown(KeyCode.Space);
    public bool PickupButton => Input.GetKeyDown(KeyCode.E);

    private void Awake()
    {
        Instance = this;
    }
    
}