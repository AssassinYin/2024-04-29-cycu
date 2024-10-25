using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class StarTreeSMOL : MonoBehaviour
{
    // Start is called before the first frame update

    public enum Status { idle, walk };
    public Status status;


    public enum Facing { left, right };
    public Facing facing;
    [SerializeField] public float speed;
    private Transform Mytransform;
    public Transform playerTrans;
    public SpriteRenderer spr;


    public bool canChangeState;

  
    

    Rigidbody2D rigid2D;
    public float maxSpeedX = 20f;

    [SerializeField] public Animator anime;



    void Start()
    {

        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        canChangeState = true;

        status = Status.idle;

        //changeState();
        if ( spr.flipX == false ) {
            facing = Facing.left;
        }
        else {
            facing = Facing.right;
        }


        Mytransform = this.transform;

        if ( GameObject.FindWithTag("Player") != null ) {
            playerTrans = GameObject.FindWithTag("Player").transform;
        }   

             
    }

    void Update() {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        switch(status) {
            
            case Status.idle: // start status -> stand
                if ( canChangeState ) {

                    checkAni();
                    changeState();
                }
   
                break;
            case Status.walk: // walk status
                
                if ( canChangeState ){
                    walking();
                    checkAni();
                    changeState();

                }             

                break;

        
        }

    }

    void walking() {
        setFaceing();

        print("walking");
        switch( facing ) {
            case Facing.right:
                 rigid2D.AddForce( new Vector2( 100*speed, 0 ), ForceMode2D.Force );

                //Mytransform.position += new Vector3( speed * deltaTime, 0, 0 );
                if ( rigid2D.velocity.x > maxSpeedX ) {
                    rigid2D.velocity = new Vector2( maxSpeedX, rigid2D.velocity.y );
                }
                break;
            case Facing.left:
                rigid2D.AddForce( new Vector2( -100*speed, 0 ), ForceMode2D.Force );
                //Mytransform.position -= new Vector3( speed * deltaTime, 0, 0 );
                if ( rigid2D.velocity.x < -maxSpeedX ) {
                    rigid2D.velocity = new Vector2( -maxSpeedX, rigid2D.velocity.y );
                }                        
                break;
        }

    }

    public void changeState() {

        if ( playerTrans && canChangeState ) {
            if ( Mathf.Abs( Mytransform.position.x - playerTrans.position.x ) > 80f  ) {
                status = Status.idle;
                
            }
            else {
                status = Status.walk;
            }
        }
        else { 
            status = Status.idle;
        }
    }


    void checkAni() {
        // 0:idle 1: walk 2:dash 3:attack
        if ( status == Status.idle ) {
            anime.SetInteger( "status", 0 );

        }
        else if ( status == Status.walk ) {
            anime.SetInteger( "status", 1 );

        }

    }









    private void setFaceing() {
        if ( playerTrans ) {
            if ( Mytransform.position.x >= playerTrans.position.x ) {
                facing = Facing.left;
                spr.flipX = false;
            }
            else {
                facing = Facing.right;
                spr.flipX = true;

                }
        }
    }



}
