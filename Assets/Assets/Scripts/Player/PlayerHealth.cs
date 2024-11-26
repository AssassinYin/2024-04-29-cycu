using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : EntityHealth
{
    private bool _hurtStopTime = false;
    public bool HurtStopTime { get { return _hurtStopTime; } }

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

            //vanish state

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(StartInvulnerableFrame());
                StartCoroutine(StartHurtStopFrame());
            }
        }
    }
}
