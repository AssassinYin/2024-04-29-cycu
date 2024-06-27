using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {
        Debug.Log("CheckSwitchState IDLE STATE");
        if (_context.CanDash() && _context.LastPressedDashTime > 0)
            SwitchState(_factory.Dash());

        else if (_context.MoveInput.x != 0)
            SwitchState(_factory.Walk());
    }
    public override void EnterState()
    {
        Debug.Log("IDLE STATE");
    }
    public override void ExitState()
    {
        
    }
    public override void UpdateState()
    {
        
    }
    public override void InitializeSubState()
    {
        
    }
}
