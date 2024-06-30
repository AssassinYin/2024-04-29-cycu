using UnityEngine;

public class PlayerAttackActionState : PlayerBaseState
{
    public PlayerAttackActionState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {

    }
    public override void EnterState()
    {
        Debug.Log("GROUNDED STATE");
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
