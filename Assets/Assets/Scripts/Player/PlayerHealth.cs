using System.Collections;
using UnityEngine;

public class PlayerHealth : EntityHealth
{
    private bool _hurtStopTime = false;
    public bool HurtStopTime { get { return _hurtStopTime; } }
    //ª±®a­µ®Ä
    public AudioClip clip;

    public PlayerMovement PlayerMovement { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    public virtual IEnumerator StartHurtStopFrame()
    {
        //wait for the amount of invulnerableFrame
        _hurtStopTime = true;
        yield return new WaitForSeconds(invulnerableFrame * 0.5f);
        //turn off the hit bool so the enemy can receive damage again
        _hurtStopTime = false;
    }

    public override void ApplyDamage(int amount)
    {
        //First checks to see if the player is currently in an invulnerable state; if not it runs the following logic.
        if (!_inInvulnerableFrame && !isInvulnerable)
        {

            _currentHealth -= amount;

            if (_currentHealth > healthAmount)
            {
                _currentHealth = healthAmount;
            }

            if (slider != null)
                slider.value = _currentHealth;

            SoundManager.instance.PlaySound(clip);

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                gameObject.SetActive(false);
            }
            else
            {
                PlayerMovement.Sleep(0.2f);
                StartCoroutine(StartInvulnerableFrame());
                StartCoroutine(StartHurtStopFrame());
            }
        }
    }
}
