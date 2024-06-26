using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starTree : MonoBehaviour
{
    // Start is called before the first frame update

    public enum Status { idle, walk };
    public Status status;
    public enum Facing { left, right};
    public Facing facing;
    public float speed;
    private Transform Mytransform;
    public Transform playerTrans;
    private SpriteRenderer spr;
    void Start()
    {
        spr = this.transform.GetComponent<SpriteRenderer>();
        status = Status.idle;
        if ( spr.flipX == false ) {
            facing = Facing.left;
        }
        else {
            facing = Facing.right;
        }

        Mytransform = this.transform;
        if ( GameObject.Find("player") != null ) {
            playerTrans = GameObject.Find("player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        print( Mathf.Abs( Mytransform.position.x - playerTrans.position.x ) );
        switch(status) {
            case Status.idle:
                if ( playerTrans ) {
                    if ( Mathf.Abs( Mytransform.position.x - playerTrans.position.x ) < 20f ) {
                        status = Status.walk;
                    }
                }
                break;
            case Status.walk:

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

                switch( facing ) {
                    case Facing.right:
                        Mytransform.position += new Vector3( speed * deltaTime, 0, 0 );
                        break;
                    case Facing.left:
                        Mytransform.position -= new Vector3( speed * deltaTime, 0, 0 );
                        break;
                }

                if ( playerTrans ) {
                    if ( Mathf.Abs( Mytransform.position.x - playerTrans.position.x ) >= 5f  ) {
                        status = Status.idle;
                    }
                }

                break;
        }
    }
}
