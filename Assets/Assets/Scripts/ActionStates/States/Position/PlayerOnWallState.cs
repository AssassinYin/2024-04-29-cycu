using UnityEngine;

public class PlayerOnWallState : PlayerBaseState
{
    public PlayerOnWallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {
        if (_context.IsOnGround())
            SwitchState(_factory.Grounded());

        else if (!_context.IsOnGround() && (!(_context.IsOnRightWall() || _context.IsOnLeftWall())))
            SwitchState(_factory.Airborne());
    }
    public override void EnterState()
    {
        Debug.Log("ONWALL STATE");
    }
    public override void UpdateState()
    {
        if (_context.IsOnRightWall())
            _context.OnRightWall();

        else
            _context.OnLeftWall();

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
