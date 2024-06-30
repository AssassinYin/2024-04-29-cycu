public class PlayerStateFactory
{
    private PlayerStateMachine _context;
    public PlayerStateFactory(PlayerStateMachine playerStateMachine)
    {
        _context = playerStateMachine;
    }
    #region POSITION
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }
    public PlayerBaseState Airborne()
    {
        return new PlayerIdleJumpState(_context, this);
    }
    public PlayerBaseState OnWall()
    {
        return new PlayerDoubleJumpState(_context, this);
    }
    #endregion POSITION

    #region MOVEMENT
    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Walk()
    {
        return new PlayerMoveState(_context, this);
    }
    public PlayerBaseState Dash()
    {
        return new PlayerDashState(_context, this);
    }
    public PlayerBaseState Slide()
    {
        return new PlayerSlideState(_context, this);
    }
    #endregion POSITION
    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }
    public PlayerBaseState WallJump()
    {
        return new PlayerWallJumpState(_context, this);
    }
}

