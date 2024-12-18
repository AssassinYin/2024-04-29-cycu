using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class StarBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 50f;
    Rigidbody2D rd;

    private SpriteRenderer starTreeSpr;
    public Transform starTree;

    bool facingLeft;

    
    void Start()
    {
       rd = this.gameObject.GetComponent<Rigidbody2D>();
       rd.gravityScale = 0f;

       
        if ( GameObject.FindWithTag("Foe") != null ) {
            starTree = GameObject.FindWithTag("Foe").transform;
        } 
       
       starTreeSpr = starTree.GetComponent<SpriteRenderer>(); 
       setVertex();


        // �����𭵮�
        if (SoundManager.instance != null)
        {
            SoundManager.instance.startree_PlayAttackSound("throwStar");
        }
        else
        {
            Debug.LogWarning("SoundManager instance not found!");
        }

    }

    // Update is called once per frame   
    void FixedUpdate()
    {
        setForce();
    }

    void setVertex() {
        if ( starTreeSpr.flipX == false ) {
            facingLeft = true;
            rd.AddForce( new Vector2( -1 * speed, rd.velocity.y ), ForceMode2D.Impulse );
        }
        else {
            facingLeft = false;
            rd.AddForce( new Vector2( speed, rd.velocity.y ), ForceMode2D.Impulse );
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

        if ( /*coll.CompareTag("Foe") ||*/ coll.CompareTag("Player") || coll.CompareTag("deleteLine") ) {
         
            if ( coll.CompareTag("Player")) {
                coll.GetComponent<PlayerHealth>().ApplyDamage(10);
            }
      
            Destroy( this.gameObject ); 
      
        }

       
    }
}
