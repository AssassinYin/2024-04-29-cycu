using UnityEngine;

public class PlayerIdleActionState : PlayerBaseState
{
    public PlayerIdleActionState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {

    }
    public override void EnterState()
    {
        Debug.Log("AIRBORNE STATE");
    }
    public override void UpdateState()
    {
        
    }
    public override void ExitState()
    {
        
    }
    public override void InitializeSubState()
    {

    }
}
