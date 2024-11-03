using UnityEngine;

public class RegenerateCan : Can
{
    public override void ApplyDamage(int amount)
    {
        isReflected = true;
        ApplyKnockback(-dir);
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
            eh.ApplyDamage(-10);
            Destroy(gameObject);
        }
    }
}