using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerStateMachine _context;
    protected PlayerStateFactory _factory;
    protected PlayerBaseState _superState, _subState;
    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    {
        _context = currentContext;
        _factory = playerStateFactory;
        InitializeSubState();
    }

    #region ABSTRACT METHODS
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchState();
    public abstract void InitializeSubState();
    #endregion ABSTRACT METHODS

    #region GENERAL METHODS
    public void UpdateStates()
    {
        UpdateState();
        CheckSwitchState();
        _subState?.UpdateStates();
    }
    protected void SwitchState(PlayerBaseState newState)
    {
        //current state exit
        ExitState();

        //entering new state
        newState.EnterState();
        if (_superState != null) Debug.Log("NOT BASE STATE");
        else Debug.Log("BASE STATE");

        //switching state
        if (_superState != null)
            _superState?.SetSubState(newState);
        else
            _context.CurrentState = newState;
    }
    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _superState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState)
    {
        Debug.Log("HI STATE");
        _subState = newSubState;
        newSubState.SetSuperState(this);
    }
    #endregion GENERAL METHODS
}
