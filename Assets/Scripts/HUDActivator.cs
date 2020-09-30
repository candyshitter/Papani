public class HUDActivator : PanelActivator
{
    public override bool BoolState(IState state) => state is Play;
}