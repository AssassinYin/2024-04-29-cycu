using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterType
{
    Player,
    Monster1,
}



public class EntityHealth : MonoBehaviour
{
    [SerializeField] private CharacterType characterType;
    [SerializeField] protected Slider slider;
    //if this GameObject should receive damage or not
    [SerializeField] protected bool isInvulnerable = true;
    //total number of health points the GameObject should have
    [SerializeField] protected int healthAmount = 100;
    //time before entity can receive damage again
    [SerializeField] protected float invulnerableFrame = .2f;

    [SerializeField] protected bool _inInvulnerableFrame;
    [SerializeField] protected int _currentHealth;
    protected Rigidbody2D _rigidbody;

    public bool InInvulnerableFrame { get { return _inInvulnerableFrame; } }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (slider != null)
        {
            slider.maxValue = healthAmount;
            slider.value = healthAmount;
        }
    }

    private void Start()
    {
        //sets the enemy to the max amount of health when the scene loads
        _currentHealth = healthAmount;
    }

    private void Update()
    {
        Vector3 playerTrans;
        if (GameObject.FindWithTag("Player") != null)
        {
            playerTrans = GameObject.FindWithTag("Player").transform.position;

            if (slider != null)
            {
                if (Vector3.Distance(playerTrans, transform.position) < 25)
                    slider.gameObject.SetActive(true);

                else
                    slider.gameObject.SetActive(false);
            }
        }
    }

    public virtual void ApplyDamage(int amount)
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
                // 播放角色受傷音效
                SoundManager.instance.PlayHurtSound(characterType);
                StartCoroutine(StartInvulnerableFrame());
            } 
            
            
            //StartCoroutine(StartInvulnerableFrame());
        }
    }


    public virtual void ApplyKnockback(Vector2 dir)
    {
        _rigidbody.AddForce(dir, ForceMode2D.Impulse);
    }

    public IEnumerator StartInvulnerableFrame()
    {
        //wait for the amount of invulnerableFrame
        isInvulnerable = true;
        _inInvulnerableFrame = true;
        yield return new WaitForSeconds(invulnerableFrame);
        //turn off the hit bool so the enemy can receive damage again
        _inInvulnerableFrame = false;
        isInvulnerable = false;
    }

    public int getCurHP() {
        return _currentHealth;
    }

    private void TestHurtSound()
    {
        Debug.Log($"Testing hurt sound for {characterType}");
        SoundManager.instance.PlayHurtSound(characterType);
    }

    public virtual void ApplySPDamage(int amount)
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
            // 播放角色受傷音效
            SoundManager.instance.PlayHurtSound(characterType);
        }

    }
    
}