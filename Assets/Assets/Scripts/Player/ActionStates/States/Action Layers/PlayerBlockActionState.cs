using UnityEngine;

public class PlayerBlockActionState : PlayerBaseState
{
    public PlayerBlockActionState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }

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
    private IEnumerator StartBlock()
    {
        //ensures can't call block multiple times from one press
        LastPressedBlockTime = 0;
        LastPressedJumpTime = 0;

        //block ready phase
        float startTime = Time.time;
        while (Time.time - startTime <= Data.blockReadyTime)
            yield return null;

        BoxCollider2D collider2D = Block.GetComponent<BoxCollider2D>();
        //blocking precise phase
        collider2D.enabled = true;
        collider2D.size = new Vector2(1.5f, 1.5f);
        startTime = Time.time;
        while (Time.time - startTime <= Data.blockPreciseTime)
            yield return null;

        //blocking phase
        collider2D.size = new Vector2(1.25f, 1.25f);
        startTime = Time.time;
        Block.GetComponent<Collider2D>().enabled = true;
        while (Time.time - startTime <= Data.blockTime)
            yield return null;

        //block end phase
        startTime = Time.time;
        collider2D.enabled = false;
        while (Time.time - startTime <= Data.blockEndTime)
            yield return null;

        //block over
        IsBlocking = false;
    } */
}
