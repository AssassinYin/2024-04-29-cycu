using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoesHealth : EntityHealth
{
    // Update is called once per frame
    


    private void Patroling()
    {
        
    }

    void OnCollisionStay2D(Collision2D coll ) {
        if ( coll.gameObject.CompareTag("Player")) {
            coll.gameObject.GetComponent<PlayerHealth>().ApplyDamage(1);
        }
    }

}
