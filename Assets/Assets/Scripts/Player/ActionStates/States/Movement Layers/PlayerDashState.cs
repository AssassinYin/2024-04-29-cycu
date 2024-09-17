using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {

    }

    public override void EnterState()
    {
        Debug.Log("DASH STATE");
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
        //ensures can't call Dash multiple times from one press
        LastOnGroundTime = 0;
        LastPressedDashTime = 0;

        float startTime = Time.time;

        _dashesLeft--;
        _isDashAttacking = true;

        SetGravityScale(0);

        //keep the player's velocity at the dash speed during the "attack" phase
        while (Time.time - startTime <= Data.dashAttackTime)
        {
            Rigidbody.velocity = dir.normalized * Data.dashSpeed;
            //pauses the loop until the next frame, creating something of a Update loop
            yield return null;
        }

        startTime = Time.time;

        _isDashAttacking = false;

        //begins the "end" of dash where return some control to the player but still limit run acceleration
        SetGravityScale(Data.gravityScale);
        Rigidbody.velocity = Data.dashEndSpeed * dir.normalized;

        while (Time.time - startTime <= Data.dashEndTime)
            yield return null;

        //dash over
        IsDashing = false;
    }
    */
}
