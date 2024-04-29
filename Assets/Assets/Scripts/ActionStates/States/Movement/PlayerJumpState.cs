using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {
        if (_context.CanDash() && _context.LastPressedDashTime > 0)
            SwitchState(_factory.Dash());

        else if (_superState is PlayerOnWallState)
        {
            if ((_context.MoveInput.x < 0) || (_context.MoveInput.x > 0))
                SwitchState(_factory.Slide());

            else if (_context.LastPressedJumpTime > 0)
                SwitchState(_factory.WallJump());
        }

        else if (_superState is not PlayerAirborneState && _context.LastPressedJumpTime > 0)
            SwitchState(_factory.Jump());

        else if (_context.MoveInput == Vector2.zero)
            SwitchState(_factory.Idle());
    }
    public override void EnterState()
    {
        Debug.Log("JUMP STATE");
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        
    }
    /*
    private void Jump()
    {
        //ensures can't call Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;

        #region Perform Jump
        //increase the force applied if we are falling, will always feel like jump same amount.
        float force = Data.jumpForce;
        if (Rigidbody.velocity.y < 0)
            force -= Rigidbody.velocity.y;

        Rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        #endregion
    }
    */
}
