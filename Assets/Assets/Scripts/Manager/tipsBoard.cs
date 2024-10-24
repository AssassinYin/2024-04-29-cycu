using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tipsBoard : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject tips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if ( Input.GetKeyDown( KeyCode.L ) ) {
            print("open tips!");
            tips.SetActive(true);
        } 
        */

    }

    void OnTriggerStay2D( Collider2D coll ) {
        if ( coll.CompareTag( "Player" ) ) {

            if ( tips != null ) {
                tips.SetActive(true);
            }
            else {
                print("tips is empty!!");
            }

            
        }
    }
}
