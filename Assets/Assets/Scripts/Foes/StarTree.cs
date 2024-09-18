using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class StarTree : MonoBehaviour
{
    // Start is called before the first frame update

    public enum Status { idle, walk, dash, attack, waiting };
    public Status status;

    public Status lastStatus;
    public enum Facing { left, right };
    public Facing facing;
    public float speed;
    private Transform Mytransform;
    public Transform playerTrans;
    public SpriteRenderer spr;

    private bool canDash = true;
    private bool isDasing;
    private float dashingPower = 24f;
    private float dashingTime = 0.5f;
    private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private GameObject digPrefab1;

    [SerializeField] private GameObject digPrefab2;
    [SerializeField] private GameObject digPrefab3;

    public bool canChangeState;

    private enum AttackType { throwStar, spin, dig, waiting };

    private AttackType attackType;
    public bool isAttacking = false;

    public bool throwing = false;

    public bool spinning = false;

    public bool digging = false;

    private float digCooldown = 10.0f;
    private float throwAttackTime = 0.5f;

    private float throwCooldown = 10f;
    private float throwPrepTime = 1.0f;

    private float spinAttackTime = 3.0f;

    private float spinCooldown = 10f;
    

    Rigidbody2D rigid2D;
    public float maxSpeedX;

    [SerializeField] public Animator anime;

    private bool preAtkDone = false;


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
<<<<<<< HEAD:Assets/Assets/Scripts/Foes/StarTree.cs
        if ( GameObject.Find("Player") != null ) {
            playerTrans = GameObject.Find("Player").transform;
=======

        if ( GameObject.FindWithTag("Player") != null ) {
            playerTrans = GameObject.FindWithTag("Player").transform;
        }   

             
    }

    void Update() {
        /*
        if ( Input.GetKeyDown( KeyCode.X ) ) {
            //Instantiate( bulletPrefab, new Vector2 ( this.transform.position.x - 5,this.transform.position.y ), Quaternion.identity );
            //throwAttack();
            attackType = AttackType.throwStar;
            status = Status.attack;
>>>>>>> mob:Assets/Assets/Scripts/mobs/starTree.cs
        }
        */
        /* dead
        else if ( Input.GetKeyDown( KeyCode.C ) ) {

            attackType = AttackType.spin;
            status = Status.attack;
        }
        */
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(canChangeState + "/" + isAttacking );
        
        //float deltaTime = Time.deltaTime;
        //print( Mathf.Abs( Mytransform.position.x - playerTrans.position.x ) );
        //print( rigid2D.velocity );
        //print( dashCD );
        
        switch(status) {
            
            case Status.idle: // start status -> stand
                if ( canChangeState ) {
                    lastStatus = Status.idle;
                    checkAni();
                    changeState();
                }
   
                break;
            case Status.walk: // walk status
                if ( canChangeState ){
                    lastStatus = Status.idle;
                    checkAni();
                    walking();

                    changeState();

                }             

                break;

            case Status.dash:
                if ( canChangeState ) {
                    
                    if ( canDash ) {
                        lastStatus = Status.dash;
                        checkAni();
                        //print( "Execute");
                        StartCoroutine( Dash() );
                    }
                    changeState();
                }


                break;

            case Status.attack:
                
                if ( attackType == AttackType.spin ) {
                    walking();
                }

                if ( canChangeState ) {
                        
                    //set attackType
                    //int random = 1;
                    
                    //print( random + "/" + attackType );
                      
                    if ( isAttacking == false ) {    
                        int random = Random.Range(1,5); // 1~4
                        attackType = AttackType.waiting;    
                        
                        if ( random == 1 && throwing == false ) {
                            attackType = AttackType.throwStar;
                        }
                        else if ( random == 2 && spinning == false ) {
                            attackType = AttackType.spin;
                        }
                        else if ( random == 3 && digging == false ) {
                            attackType = AttackType.dig;
                        }
                        else if ( random == 4 && canDash == true ) {
                            status = Status.dash;
                            break;
                            
                        }
                        checkAni();
                        
                        if ( attackType != AttackType.waiting ) {
                            lastStatus = Status.attack;
                            StartCoroutine( Attack() );
                             
                            
                        }
                        
                    
                    }
                    changeState(); 
                                                        
                }
                

                break;

            /*case Status.waiting:
            print( "waiting" );
                if ( attackType == AttackType.spin ) {
                    walking();
                }
                lastStatus = Status.waiting;
                changeState();

                break;
            */

        
        }

    }

    void walking() {
        setFaceing();

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

            else if ( Mathf.Abs( Mytransform.position.x - playerTrans.position.x ) <= 20f && isAttacking == false /*&&
                      lastStatus != Status.attack*/ ) {
                status = Status.attack;
            }

            else if ( Mathf.Abs( Mytransform.position.x - playerTrans.position.x ) <= 80f && 
                    Mathf.Abs( Mytransform.position.x - playerTrans.position.x ) > 0f ) {
                status = Status.walk;
            }
            else {
                status = Status.walk;
            }
        }
        else { // if it's attacking
            status = Status.walk;
        }
    }


    void checkAni() {
        // 0:idle 1: walk 2:dash 3:attack
        //print( lastStatus + " / " + status );
        if ( status == Status.idle ) {
            anime.SetInteger( "status", 0 );
            //print("0");
        }
        else if ( status == Status.walk ) {
            anime.SetInteger( "status", 1 );
            //print("1");
        }
        else if ( status == Status.dash ) {
            anime.SetInteger( "status", 2 );
            //print("2");
        }
        else if ( status == Status.attack && lastStatus != Status.attack ) {
            if ( attackType == AttackType.throwStar )  {
                anime.SetInteger( "status", 3 ); //prep
            }
            else if ( attackType == AttackType.spin ) {
                anime.SetInteger( "status", 5 );
            }
            else if ( attackType == AttackType.dig ) {
                anime.SetInteger( "status", 6 );
            }
            else {
                anime.SetInteger( "status", 0 ); 
            }
            
            
        }
        else {
            anime.SetInteger( "status", 0 ); 
             
        }
    }


    private IEnumerator Dash() {
        canDash = false;
        isDasing = true;
        float originalGravity = rigid2D.gravityScale;
        canChangeState = false;

        yield return new WaitForSeconds( 1f );
        
        rigid2D.gravityScale = 0f;
        if ( facing == Facing.right ) {
            rigid2D.velocity = new Vector2( transform.localScale.x * dashingPower, 0f );
        }
        else if ( facing == Facing.left ) {
            rigid2D.velocity = new Vector2( -transform.localScale.x * dashingPower, 0f );
        }
        
        tr.emitting = true;

        yield return new WaitForSeconds( dashingTime );

        canChangeState = true;
                if ( playerTrans ) {
                    if ( Mathf.Abs( Mytransform.position.x - playerTrans.position.x ) >= 20f  ) {
                        status = Status.idle;
                        
                    }
                    else if ( Mathf.Abs( Mytransform.position.x - playerTrans.position.x ) < 20f ) {
                        status = Status.walk;
                    }
                }        
        
        tr.emitting = false;
        rigid2D.gravityScale = originalGravity;
        isDasing = false;
        
        yield return new WaitForSeconds( dashingCooldown );
        canDash = true;

    }

    private IEnumerator Attack() {
        canChangeState = false;
        isAttacking = true;
        
        //AnimatorClipInfo[] clipInfo;
        AnimationClip[] clips;
        AnimationClip clip;
        float animationLength = 0f;

        switch( attackType ) {
            
            case AttackType.throwStar:
            
                throwing = true;
                clips = anime.runtimeAnimatorController.animationClips;
                clip = FindClipByName( clips, "throwPrep" );
                if ( clip != null ) {
                    animationLength = clip.length;
                }
                else {
                    animationLength = 0;
                }
                clip = FindClipByName( clips, "starTreeThrow" );
                if ( clip != null ) {
                    animationLength += clip.length;
                }
                else {
                    animationLength += 0;
                }

                yield return new WaitForSeconds( animationLength );

                //changeToStand();
                //yield return new WaitForSeconds( 1f );
                //changeState();
                canChangeState = true;

                yield return new WaitForSeconds( 2f );

                isAttacking = false;
                

                yield return new WaitForSeconds( throwCooldown - 1f );
                throwing = false;
                //isAttacking = false;

            break;
            

            case AttackType.spin:
                spinning = true;
                //walking();
                clips = anime.runtimeAnimatorController.animationClips;
                clip = FindClipByName( clips, "starTreeSpin" );
                if ( clip != null ) {
                    animationLength = clip.length;
                }
                else {
                    animationLength = 0;
                }

                yield return new WaitForSeconds( animationLength );

                //changeToStand();
                //yield return new WaitForSeconds( 1f );
                //changeState();
                canChangeState = true;

                yield return new WaitForSeconds( 2f );
                isAttacking = false;
                
                
                yield return new WaitForSeconds( spinCooldown - 1f );
                spinning = false;
                //isAttacking = false;
                
         
            break;

            case AttackType.dig:
                digging = true;

                clips = anime.runtimeAnimatorController.animationClips;
                clip = FindClipByName( clips, "starTreeDig" );
                if ( clip != null ) {
                    animationLength = clip.length;
                }
                else {
                    animationLength = 0;
                }
                yield return new WaitForSeconds( animationLength );

                //changeToStand();
                //yield return new WaitForSeconds( 1f );
                //changeState();
                canChangeState = true; 

                yield return new WaitForSeconds( 2f );

                isAttacking = false;
                          
                
                yield return new WaitForSeconds( digCooldown - 1f );
                digging = false;   
                //isAttacking = false; 



            break;


        }


       
    }

    public void changeToStand() {
        status = Status.idle;
        anime.SetInteger( "status", 0 );
    }

    public void throwAttack() {
        if ( facing == Facing.left ) {
            Instantiate( bulletPrefab, new Vector2 ( this.transform.position.x - 5,this.transform.position.y ), Quaternion.identity );
        }
        else {
            Instantiate( bulletPrefab, new Vector2 ( this.transform.position.x + 5,this.transform.position.y ), Quaternion.identity );
        }

    }

    public void digAttack() {
        if ( facing == Facing.left ) {
            Instantiate( digPrefab1, new Vector2 ( this.transform.position.x - 5,this.transform.position.y-5 ), Quaternion.identity );
            Instantiate( digPrefab2, new Vector2 ( this.transform.position.x - 5,this.transform.position.y-5 ), Quaternion.identity );
            Instantiate( digPrefab3, new Vector2 ( this.transform.position.x - 5,this.transform.position.y-5 ), Quaternion.identity );
            
        }
        else if ( facing == Facing.right ) {
            Instantiate( digPrefab1, new Vector2 ( this.transform.position.x + 5,this.transform.position.y-5 ), Quaternion.identity );
            Instantiate( digPrefab2, new Vector2 ( this.transform.position.x + 5,this.transform.position.y-5 ), Quaternion.identity );
            Instantiate( digPrefab3, new Vector2 ( this.transform.position.x + 5,this.transform.position.y-5 ), Quaternion.identity );
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

    AnimationClip FindClipByName(AnimationClip[] clips, string name)
    {
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        Debug.LogWarning("Animation clip " + name + " not found.");
        return null;
    }

    public void setIsAttackToF() {
        isAttacking = false;
    }

}
