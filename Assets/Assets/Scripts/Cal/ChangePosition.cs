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

    [SerializeField] private GameObject Boss;
    private Rigidbody2D bossRB;
    private EntityHealth curHP;

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
        if( hitCount == 5 ) {
            hitCount = 0;
            teleport();
        }
    }

    void teleport() {
        int pos = Random.Range( 1, 6 );
        if ( pos == 1 ) {
            bossRB.position = posLB.transform.position;
        }
        else if ( pos == 2 ) {
            bossRB.position = posRB.transform.position;
        }
        else if ( pos == 3 ) {
            bossRB.position = posMID.transform.position;
        }
        else if ( pos == 4 ) {
            bossRB.position = posLU.transform.position;
        }
        else if ( pos == 5 ) {
            bossRB.position = posRU.transform.position;
        }
    }
}
