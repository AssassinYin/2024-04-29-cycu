using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Callbacks;
using UnityEngine;

public class TPPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject target;
    [SerializeField] GameObject player;

    private Rigidbody2D playerRB;

    private Vector2 targetPos;

    private BoxCollider2D playerBX;


    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        playerBX = player.GetComponent<BoxCollider2D>();
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D( Collider2D coll ) {
        if ( coll.CompareTag( "Player" ) ) {
            /*
            playerBX.enabled = false;
            playerRB.MovePosition( targetPos );
            playerBX.enabled = true;
            */
            playerRB.position = targetPos;
        }
    }
}
