using UnityEngine;

public class Can : FoesHealth
{
    public Vector2 dir = Vector2.zero;
    protected Rigidbody2D rb;
    private BoxCollider2D bc;
    protected bool isReflected, isAddforce;

    private void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        isReflected = false;
        isAddforce = false;
        Destroy(gameObject, 6f);
    }
    
    private void FixedUpdate() {
        if (!isAddforce && dir != Vector2.zero) {
            rb.AddForce(dir, ForceMode2D.Impulse);
            isAddforce = true;
        }
    }
}
