using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCan : MonoBehaviour
{
    [SerializeField] CollectManager cm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D( Collider2D coll ) {
        if ( coll.gameObject.CompareTag( "Player" ) ) {
            cm.countPlusOne();
            Destroy( gameObject );
        }

    }    
}
