using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoesHealth : EntityHealth
{
    // Update is called once per frame
    
    void Update()
    {

    }

    private void Patroling()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll ) {
        if ( coll.gameObject.CompareTag("Player")) {
            coll.gameObject.GetComponent<PlayerHealth>().ApplyDamage(10);
        }
    }

}
