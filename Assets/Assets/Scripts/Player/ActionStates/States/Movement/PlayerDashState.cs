using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {
        if (!_context.IsDashing)
            if (_context.MoveInput.x != 0)
                SwitchState(_factory.Walk());

            else
                SwitchState(_factory.Idle());
    }

    public override void EnterState()
    {
        Debug.Log("DASH STATE");
        /*
        #region DASH CHECKS
        if (CanDash() && LastPressedDashTime > 0)
        {
            //freeze game for split second. Adds juiciness and a bit of forgiveness over directional input.
            Sleep(Data.dashSleepTime);

            //if not direction pressed, dash forward.
            if (MoveInput != Vector2.zero)
                _lastDashDir = MoveInput;

            else
                _lastDashDir = IsFacingRight ? Vector2.right : Vector2.left;

            IsDashing = true;
            IsJumping = false;
            IsWallJumping = false;
            _isJumpCut = false;

            StartCoroutine(nameof(StartDash), _lastDashDir);
        }
        #endregion
        */
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
    private IEnumerator StartDash(Vector2 dir)
    {
        _context.LastOnGroundTime = 0;
        _context.LastPressedDashTime = 0;

        float _context.startTime = Time.time;

        _context._dashesLeft--;
        _context._isDashAttacking = true;

        _context.SetGravityScale(0);

        //keep the player's velocity at the dash speed during the "attack" phase
        while (Time.time - _context.startTime <= _context.Data.dashAttackTime)
        {
            _context.Rigidbody.velocity = dir.normalized * _context.Data.dashSpeed;
            //pauses the loop until the next frame, creating something of a Update loop
            yield return null;
        }

        _context.startTime = Time.time;

        _context._isDashAttacking = false;

        //begins the "end" of dash where return some control to the player but still limit run acceleration
        _context.SetGravityScale(Data.gravityScale);
        _context.Rigidbody.velocity = _context.Data.dashEndSpeed * dir.normalized;

        while (Time.time - startTime <= Data.dashEndTime)
            yield return null;

        //dash over
        IsDashing = false;
    }
    */
}
