using UnityEngine;

public class PlayerSlideState : PlayerBaseState
{
    public PlayerSlideState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

    public override void CheckSwitchState()
    {

    }

    public override void EnterState()
    {
        Debug.Log("SLIDE STATE");
    }

    public override void ExitState()
    {
        
    }

    public override void InitializeSubState()
    {
        
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
    /*
         private void Slide()
    {
        //works the same as the Run but only in the y-axis
        float speedDif = Data.slideSpeed - Rigidbody.velocity.y;
        float movement = speedDif * Data.slideAccel;

        //clamp the movement here to prevent any over corrections (these aren't noticeable in the Run)
        //force applied can't be greater than the (negative) speedDifference * by how many times a second FixedUpdate() is called
        //more detail in how force are applied to rigidbodies
        movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime),
                                          Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));

        Rigidbody.AddForce(movement * Vector2.up);
    }
     */
}
