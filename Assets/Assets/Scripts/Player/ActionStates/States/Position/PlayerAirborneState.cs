using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    public PlayerAirborneState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {
        if (_context.IsOnRightWall() || _context.IsOnLeftWall())
            SwitchState(_factory.OnWall());

        else if (_context.IsOnGround())
            SwitchState(_factory.Grounded());
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
        if (_context.CanDash() && _context.LastPressedDashTime > 0)
            SwitchState(_factory.Dash());

        else if (_context.MoveInput.x != 0)
            SwitchState(_factory.Walk());

        else
            SwitchState(_factory.Idle());
    }
}
