using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] private Slider slider;
    //if this GameObject should receive damage or not
    [SerializeField] private bool isInvulnerable = true;
    //total number of health points the GameObject should have
    [SerializeField] private int healthAmount = 100;
    //time before entity can receive damage again
    [SerializeField] private float invulnerableFrame = .2f;

    private bool _inInvulnerableFrame;
    private int _currentHealth;
    private Rigidbody2D _rigidbody;

    public bool InInvulnerableFrame { get { return _inInvulnerableFrame; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //sets the enemy to the max amount of health when the scene loads
        _currentHealth = healthAmount;
    }

    public void ApplyDamage(int amount)
    {
        //First checks to see if the player is currently in an invulnerable state; if not it runs the following logic.
        if (!_inInvulnerableFrame)
        {
            _inInvulnerableFrame = true;
            _currentHealth -= amount;
            slider.value = _currentHealth;

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

    public void ApplyKnockback(Vector2 dir)
    {
        _rigidbody.AddForce(-dir, ForceMode2D.Impulse);
    }

    private IEnumerator StartInvulnerableFrame()
    {
        //wait for the amount of invulnerableFrame
        yield return new WaitForSeconds(invulnerableFrame);
        //turn off the hit bool so the enemy can receive damage again
        _inInvulnerableFrame = false;
    }
}