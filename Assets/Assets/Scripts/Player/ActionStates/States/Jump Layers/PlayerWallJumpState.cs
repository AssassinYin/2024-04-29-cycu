using UnityEngine;

public class PlayerWallJumpState : PlayerBaseState
{
    public PlayerWallJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {

    }

    public override void EnterState()
    {
        Debug.Log("");
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
    /*
         private void WallJump(int dir)
    {
        //ensures can't call wall Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;
        LastOnWallRightTime = 0;
        LastOnWallLeftTime = 0;

        #region Perform Wall Jump
        //apply force in opposite direction of wall
        Vector2 force = new Vector2(Data.wallJumpForce.x, Data.wallJumpForce.y);
        force.x *= dir;

        if (Mathf.Sign(Rigidbody.velocity.x) != Mathf.Sign(force.x))
            force.x -= Rigidbody.velocity.x;

        //checks whether player is falling, if so subtract the velocity.y (counteracting force of gravity)
        //ensures the player always reaches desired jump force or greater
        if (Rigidbody.velocity.y < 0)
            force.y -= Rigidbody.velocity.y;

        //default apply are force instantly ignoring mass
        Rigidbody.AddForce(force, ForceMode2D.Impulse);
        #endregion
    }
     */
}
