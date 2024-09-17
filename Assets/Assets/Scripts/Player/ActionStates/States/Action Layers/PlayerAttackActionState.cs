using UnityEngine;

public class PlayerAttackActionState : PlayerBaseState
{
    public PlayerAttackActionState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

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
    private IEnumerator StartAttack()
    {
        //ensures can't call attack multiple times from one press
        LastPressedAttackTime = 0;
        LastPressedJumpTime = 0;

        //attack ready phase
        float startTime = Time.time;
        while (Time.time - startTime <= Data.attackReadyTime)
            yield return null;

        //attacking phase
        startTime = Time.time;
        Attack.GetComponent<Collider2D>().enabled = true;
        while (Time.time - startTime <= Data.attackTime)
            yield return null;

        //attack end phase
        startTime = Time.time;
        Attack.GetComponent<Collider2D>().enabled = false;
        while (Time.time - startTime <= Data.attackEndTime)
            yield return null;

        //attack over
        IsAttacking = false;
    }
    private void ApplyReactionForce(Vector2 dir)
    {
        //increase the force applied if falling
        float force = Data.jumpForce;
        if (Rigidbody.velocity.y < 0)
            force -= Rigidbody.velocity.y;
        //propels the player upwards by the amount of jumpForce
        Rigidbody.AddForce(-(dir * force), ForceMode2D.Impulse);
    }
     */
}
