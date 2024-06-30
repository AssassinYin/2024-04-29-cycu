using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {

    }

    public override void EnterState()
    {
        Debug.Log("WALK STATE");
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
         private void Run(float lerpAmount)
    {
        //calculate the direction we want to move in and our desired velocity
        float targetSpeed = MoveInput.x * Data.runMaxSpeed;
        //we can reduce are control using Lerp() this smooths changes to are direction and speed
        targetSpeed = Mathf.Lerp(Rigidbody.velocity.x, targetSpeed, lerpAmount);

        #region Calculate AccelRate
        float accelRate;

        //gets an acceleration value based on if we are accelerating (includes turning) or trying to decelerate (stop).
        //as well as applying a multiplier if we're airborne.
        if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount : Data.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Data.runAccelAmount * Data.accelInAir : Data.runDeccelAmount * Data.deccelInAir;
        #endregion

        #region Add Bonus Jump Apex Acceleration
        //increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural.
        if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(Rigidbody.velocity.y) < Data.jumpHangTimeThreshold)
        {
            accelRate *= Data.jumpHangAccelerationMult;
            targetSpeed *= Data.jumpHangMaxSpeedMult;
        }
        #endregion

        #region Conserve Momentum
        //won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed.
        //prevent any deceleration from happening. (conserve current momentum)
        if (Data.doConserveMomentum && Mathf.Abs(Rigidbody.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(Rigidbody.velocity.x)
            == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
            accelRate = 0;
        #endregion

        //calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - Rigidbody.velocity.x;

        //calculate force along x-axis to apply to the player
        float movement = speedDif * accelRate;

        //convert to a vector and apply to rigidbody
        Rigidbody.AddForce(movement * Vector2.right, ForceMode2D.Force);

        // Rigidbody.velocity = new Vector2(Rigidbody.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / Rigidbody.mass, Rigidbody.velocity.y);
        // Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second.
    }


     */
}
