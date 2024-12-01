using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    public GameObject posLB;
    public GameObject posRB;
    public GameObject posMID;
    public GameObject posLU;
    public GameObject posRU;

    [SerializeField] private int prevHP;

    [SerializeField] public GameObject Boss;
    public Rigidbody2D bossRB;
    private EntityHealth curHP;

    [SerializeField] private GameObject arrayLB, arrayRB;
    [SerializeField] private GameObject arrayLU, arrayRU;




    [SerializeField] private int hitCount;

    // Start is called before the first frame update
    void Start()
    {
        bossRB = Boss.GetComponent<Rigidbody2D>();
        curHP = Boss.gameObject.GetComponent<EntityHealth>();
        hitCount = 0;
        prevHP = curHP.getCurHP();
    }

    // Update is called once per frame
    void Update()
    {
      counting();

    }

    void counting() {
        if ( prevHP != curHP.getCurHP() ) {
            hitCount++;
            prevHP = curHP.getCurHP();
        }
        if( hitCount == 3 ) {
            hitCount = 0;
            teleport();
        }
    }

    void teleport() {
        int pos = Random.Range( 1, 6 );

        if ( pos == 1 ) {

            bossRB.position = Vector2.Lerp( this.transform.position, posLB.transform.position, 1f );

        }
        else if ( pos == 2 ) {

            bossRB.position = Vector2.Lerp( transform.position, posRB.transform.position, 1f );

        }
        else if ( pos == 3 ) {
            bossRB.position = Vector2.Lerp( transform.position, posMID.transform.position, 1f );
        }
        else if ( pos == 4 ) {
            bossRB.position = Vector2.Lerp( transform.position, posLU.transform.position, 1f );

        }
        else if ( pos == 5 ) {

            bossRB.position = Vector2.Lerp( transform.position, posRU.transform.position, 1f );

        }
        arrayControl( pos );
    }

    void arrayControl( int pos ) {
        if ( pos == 1 ) {
            arrayLB.SetActive( true );
            arrayRB.SetActive( false );
            arrayLU.SetActive( false );
            arrayRU.SetActive( false );
        }
        else if ( pos == 2 ) {
            arrayLB.SetActive( false );
            arrayRB.SetActive( true );
            arrayLU.SetActive( false );
            arrayRU.SetActive( false );
        }
        else if ( pos == 4 ) {
            arrayLB.SetActive( false );
            arrayRB.SetActive( false );
            arrayLU.SetActive( true );
            arrayRU.SetActive( false );
        }
        else if ( pos == 5 ) {
            arrayLB.SetActive( false );
            arrayRB.SetActive( false );
            arrayLU.SetActive( false );
            arrayRU.SetActive( true );
        }
        else {
            arrayLB.SetActive( false );
            arrayRB.SetActive( false );
            arrayLU.SetActive( false );
            arrayRU.SetActive( false );
        }
    }

}
