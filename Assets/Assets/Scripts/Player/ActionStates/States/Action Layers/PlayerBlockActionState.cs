using UnityEngine;

public class PlayerBlockActionState : PlayerBaseState
{
    public PlayerBlockActionState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {

    }
    public override void EnterState()
    {
        Debug.Log("ONWALL STATE");
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
