using System.Collections;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    //if this GameObject should receive damage or not
    [SerializeField] private bool isInvulnerable = true;
    //total number of health points the GameObject should have
    [SerializeField] private int healthAmount = 100;
    //time before entity can receive damage again
    [SerializeField] private float invulnerableFrame = .2f;

    private bool _inInvulnerableFrame;
    private int _currentHealth;

    private void Start()
    {
        //sets the enemy to the max amount of health when the scene loads
        _currentHealth = healthAmount;
    }

    public void Damage(int amount)
    {
        //First checks to see if the player is currently in an invulnerable state; if not it runs the following logic.
        if (!_inInvulnerableFrame)
        {
            _inInvulnerableFrame = true;
            _currentHealth -= amount;

            //vanish state
            if (_currentHealth <= 0 && !isInvulnerable)
            {
                _currentHealth = 0;
                gameObject.SetActive(false);
            }
            else
                StartCoroutine(StartInvulnerableFrame());
        }
    }
    private IEnumerator StartInvulnerableFrame()
    {
        //wait for the amount of invulnerableFrame
        yield return new WaitForSeconds(invulnerableFrame);
        //turn off the hit bool so the enemy can receive damage again
        _inInvulnerableFrame = false;
    }
}