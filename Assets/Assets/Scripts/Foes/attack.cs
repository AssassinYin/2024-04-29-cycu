using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnTriggerEnter2D(Collider2D coll ) {
        if ( coll.gameObject.tag == "Player") {
            coll.gameObject.GetComponent<PlayerHealth>().ApplyDamage(10);
        }
    }

}
