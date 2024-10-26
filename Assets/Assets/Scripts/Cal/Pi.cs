using UnityEngine;

public class Pi : MonoBehaviour
{
    public Vector2 dir;
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        Destroy(gameObject, 6f);
    }

    private void FixedUpdate()
    {
        rb.AddForce(dir, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null)
        {
            EntityHealth eh = collision.gameObject.GetComponent<EntityHealth>();
            eh.ApplyDamage(10);
        }

        Destroy(gameObject);
    }
}
