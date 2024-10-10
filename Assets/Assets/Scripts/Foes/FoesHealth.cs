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

    void OnTriggerEnter2D(Collider2D coll ) {
        if ( coll.gameObject.tag == "Player") {
            coll.GetComponent<EntityHealth>().ApplyDamage(10);
        }
    }
}
