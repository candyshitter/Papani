using System;

public class StateTransition
{
    public readonly IState To;
    public readonly Func<bool> Condition;

    public StateTransition(IState to, Func<bool> condition)
    {
        To = to;
        Condition = condition;
    }
}