using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Callbacks;
using UnityEngine;

public class fallDown : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject target;
    [SerializeField] GameObject player;

    private Rigidbody2D playerRB;

    private Vector2 targetPos;

    private BoxCollider2D playerBX;

    private EntityHealth playerHP;


    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        playerBX = player.GetComponent<BoxCollider2D>();
        playerHP = player.GetComponent<EntityHealth>();
        targetPos = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D( Collider2D coll ) {
        print( "touch" );
        if ( coll.CompareTag( "Player" ) ) {
            playerHP.ApplyDamage(10);
            playerRB.position = targetPos;
        }
    }
}
