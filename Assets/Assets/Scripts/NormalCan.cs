using UnityEngine;

public class NormalCan : Can
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            EntityHealth eh = other.gameObject.GetComponent<EntityHealth>();
            eh.ApplyDamage(10);
            Destroy(gameObject);
        }
    }
}