using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class digBullet2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 100f;
    Rigidbody2D rd;

    private SpriteRenderer starTreeSpr;
    public Transform starTree;

    bool facingLeft;

    
    void Start()
    {
       rd = this.gameObject.GetComponent<Rigidbody2D>();
       rd.gravityScale = 9.8f;

       
        if ( GameObject.FindWithTag("Foe") != null ) {
            starTree = GameObject.FindWithTag("Foe").transform;
        } 
       
       starTreeSpr = starTree.GetComponent<SpriteRenderer>(); 
       setVertex();

    }

    // Update is called once per frame   
    void FixedUpdate()
    {
        setForce();
    }

    void setVertex() {
        if ( starTreeSpr.flipX == false ) { //face left
            facingLeft = true;
            rd.AddForce( new Vector2( -20f, 40f ), ForceMode2D.Impulse );
        }
        else {
            facingLeft = false;
            rd.AddForce( new Vector2( 20f, 40f ), ForceMode2D.Impulse );
        }
    }

    void setForce() {
        if ( facingLeft ) {
            rd.AddForce( new Vector2( 5f, rd.velocity.y ), ForceMode2D.Force );
        }
        else if ( facingLeft == false ) {
            rd.AddForce( new Vector2( -5f, rd.velocity.y ), ForceMode2D.Force );
        }
    }

     void OnTriggerEnter2D( Collider2D coll ) {
        if ( coll.gameObject.tag == "Foe" || coll.gameObject.tag == "Player" || coll.gameObject.tag == "deleteLine" ) {
            Destroy( this.gameObject ); 
        }
       
    }
}
