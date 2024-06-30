using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

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
    }*/
}
