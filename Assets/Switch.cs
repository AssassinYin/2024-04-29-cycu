using UnityEngine;

public class Switch : FoesHealth
{
    public Sprite On;
    public Sprite Off;
    public SpriteRenderer SpriteRenderer { get; private set; }
    public bool AnimName { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        AnimName = true;
    }

    public override void ApplyDamage(int amount)
    {
        //First checks to see if the player is currently in an invulnerable state; if not it runs the following logic.
        if (!_inInvulnerableFrame && !isInvulnerable)
        {
            if (AnimName == true) {
                AnimName = false;
                SpriteRenderer.sprite = Off;
            }

            else if (AnimName == false)
            {
                AnimName = true;
                SpriteRenderer.sprite = On;
            }

            StartCoroutine(StartInvulnerableFrame());
        }
    }
}
