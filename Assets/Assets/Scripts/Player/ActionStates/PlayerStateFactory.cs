public class PlayerStateFactory
{
    private PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine playerStateMachine)
    {
        _context = playerStateMachine;
    }

    #region HURTSTOPS
    public PlayerBaseState Hurtstop()
    {
        return new PlayerHurtstop(_context, this);
    }

    public PlayerBaseState NotHurtstop()
    {
        return new PlayerNotHurtstop(_context, this);
    }
    #endregion HURTSTOPS

    #region MOVEMENTS
    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }

    public PlayerBaseState Move()
    {
        return new PlayerMoveState(_context, this);
    }

    public PlayerBaseState Slide()
    {
        return new PlayerSlideState(_context, this);
    }

    public PlayerBaseState Dash()
    {
        return new PlayerDashState(_context, this);
    }
    #endregion MOVEMENTS

    #region JUMPS
    public PlayerBaseState IdleJump()
    {
        return new PlayerIdleJumpState(_context, this);
    }

    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }

    public PlayerBaseState DoubleJump()
    {
        return new PlayerDoubleJumpState(_context, this);
    }

    public PlayerBaseState WallJump()
    {
        return new PlayerWallJumpState(_context, this);
    }
    #endregion JUMPS

    #region ACTIONS
    public PlayerBaseState IdleAction()
    {
        return new PlayerIdleActionState(_context, this);
    }

    public PlayerBaseState Attack()
    {
        return new PlayerAttackActionState(_context, this);
    }

    public PlayerBaseState Block()
    {
        return new PlayerBlockActionState(_context, this);
    }
    #endregion ACTIONS
}

