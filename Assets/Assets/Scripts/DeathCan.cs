using UnityEngine;

public class DeathCan : Can
{
    public override void ApplyDamage(int amount)
    {
        isReflected = true;
        ApplyKnockback(dir);
    }
    
    public override void ApplyKnockback(Vector2 _dir)
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        Debug.Log("DeathCan: " + dir);
        rb.AddForce(new Vector2(-dir.x, dir.y) * 10, ForceMode2D.Impulse);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>() != null)
        {
            EntityHealth eh = collision.gameObject.GetComponent<EntityHealth>();
            eh.ApplyDamage(10);
            Destroy(gameObject);
        }

        else if (isReflected && collision.gameObject.GetComponent<FoesHealth>() != null)
        {
            EntityHealth eh = collision.gameObject.GetComponent<EntityHealth>();
            eh.ApplySPDamage(10);
            Destroy(gameObject);
        }
    }
}