using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private readonly Dictionary<IState, List<StateTransition>> _stateTransitions = new Dictionary<IState, List<StateTransition>>();
    private readonly List<StateTransition> _anyStateTransitions = new List<StateTransition>();
    public IState CurrentState { get; private set; }
    public event Action<IState> OnStateChanged;
    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        if (!_stateTransitions.ContainsKey(from))
            _stateTransitions[from] = new List<StateTransition>();
        if (!_stateTransitions.ContainsKey(to))
            _stateTransitions[to] = new List<StateTransition>();
        _stateTransitions[from].Add(new StateTransition(to, condition));
    }
    public void AddTransitionViceVerca(IState from, IState to, Func<bool> condition)
    {
        AddTransition(from, to, condition);
        AddTransition(to, from, condition);
    }
    public void AddAnyTransition(IState to, Func<bool> condition)
    {
        var stateTransition = new StateTransition(to, condition);
        _anyStateTransitions.Add(stateTransition);
    }
    public void SetState(IState state)
    {
        if (CurrentState == state) return;
        CurrentState?.OnExit();
        CurrentState = state;
        CurrentState.OnEnter();
        
        OnStateChanged?.Invoke(CurrentState);
    }

    public void UpdateStates()
    {
        var transition = CheckForTransition();
        if (transition != null) 
            SetState(transition.To);
    }

    private StateTransition CheckForTransition()
    {
        foreach (var transition in _anyStateTransitions)
            if (transition.Condition())
                return transition;
        foreach (var transition in _stateTransitions[CurrentState])
            if (transition.Condition())
                return transition;
        return null;
    }
}