using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(PanelActivator))]
public class CanvasHandler : MonoBehaviour
{
    [SerializeField, Required] private GameObject _panel;
    private PanelActivator _panelActivator;
    [SerializeField] private bool startEnabled;

    private void Awake()
    {
        _panel.SetActive(startEnabled);
        GameStateMachine.OnGameStateChanged += HandleStateChanged;
        _panelActivator = GetComponent<PanelActivator>();
    }

    private void OnDestroy()
    {
        GameStateMachine.OnGameStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(IState state)
    {
        _panel.SetActive(_panelActivator.BoolState(state));
    }
}