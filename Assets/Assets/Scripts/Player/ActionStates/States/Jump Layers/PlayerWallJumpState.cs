using UnityEngine;

public class PlayerWallJumpState : PlayerBaseState
{
    public PlayerWallJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {

    }

    public override void EnterState()
    {
        Debug.Log("");
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
