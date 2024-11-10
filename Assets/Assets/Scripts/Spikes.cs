using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D( Collider2D coll ) {
        if ( coll.gameObject.GetComponent<PlayerHealth>() != null ) {
            EntityHealth hp = coll.gameObject.GetComponent<EntityHealth>();
            hp.ApplyDamage(10);
        }

    }

}
