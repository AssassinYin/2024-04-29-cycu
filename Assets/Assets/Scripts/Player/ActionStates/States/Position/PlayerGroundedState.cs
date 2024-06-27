using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {
        if (!_context.IsOnGround())
            if (_context.IsOnRightWall() || _context.IsOnLeftWall())
                SwitchState(_factory.OnWall());

            else
                SwitchState(_factory.Airborne());
    }
    public override void EnterState()
    {
        Debug.Log("GROUNDED STATE");
    }
    public override void UpdateState()
    {
        _context.OnGround();
        _context.NotAirborne();
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
