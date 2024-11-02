using System.Collections;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Vector2 dir;
    private Rigidbody2D rb;
    public Animator Animator;
    private string currentAnimation;
    public bool IsFacingRight { get; private set; }
    public bool BombOn { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(dir, ForceMode2D.Impulse);
        ChangeAnimation("BombOn");
        StartCoroutine(Wait());
        BombOn = false;
        Destroy(gameObject, 6f);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(4f);
        BombOn = true;
        yield return null;
    }

    private void FixedUpdate()
    {
        CheckDirectionToFace(
            GameObject.FindGameObjectWithTag("Player").transform.position.x > gameObject.transform.position.x);

        if (BombOn)
        {
            ChangeAnimation("BombOn");

            if (IsFacingRight)
            {
                rb.AddForce(Vector2.right * 35, ForceMode2D.Force);
            }

            else
            {
                rb.AddForce(Vector2.left * 35, ForceMode2D.Force);
            }
        }
    }
    private void ChangeAnimation(string target)
    {
        if (currentAnimation != target)
        {
            currentAnimation = target;
            Animator.CrossFade(target, 0.1f);
        }
    }
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }
    private void Turn()
    {
        IsFacingRight = !IsFacingRight;
        //Stores scale and flips the player with y rotation.
        if (IsFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.eulerAngles.x, 0.0f, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Euler(rotator);
        }

        else
        {
            Vector3 rotator = new Vector3(transform.rotation.eulerAngles.x, 180.0f, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Euler(rotator);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<FoesHealth>() == null)
        {
            if (collision.gameObject.GetComponent<PlayerHealth>() != null)
            {
                EntityHealth eh = collision.gameObject.GetComponent<EntityHealth>();
                eh.ApplyDamage(10);
                ChangeAnimation("Death");
                Destroy(gameObject, 0.3f);
            }
        }
    }
}
