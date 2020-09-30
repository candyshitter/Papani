public class PauseActivator : PanelActivator
{
    public override bool BoolState(IState state) => state is Pause;
}