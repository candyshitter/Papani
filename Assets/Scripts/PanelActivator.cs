using UnityEngine;

public abstract class PanelActivator : MonoBehaviour
{
    public abstract bool BoolState(IState state);
}