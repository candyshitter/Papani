public class InventoryActivator : PanelActivator
{
    public override bool BoolState(IState state) => state is InventoryPause;
}