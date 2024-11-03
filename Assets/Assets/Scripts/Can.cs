using UnityEngine;

public class Can : EntityHealth
{
    public Vector2 dir;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    protected bool isReflected;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        rb.AddForce(dir, ForceMode2D.Impulse);
        isReflected = false;
        Destroy(gameObject, 6f);
    }
}
