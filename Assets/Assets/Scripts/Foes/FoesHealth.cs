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

    void  OnTriggerEnter2D(Collider2D coll ) {
        if ( coll.CompareTag("Player")) {
            coll.GetComponent<PlayerHealth>().ApplyDamage(10);
        }
    }

}
