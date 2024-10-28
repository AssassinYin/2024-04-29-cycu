using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summonFoe : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject foes;
    bool canSummon = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        summon();
    }

    void checkCondition() {
        
    }
    void summon() {
        if ( canSummon ) {
            foes.SetActive( true );
        }
    }

}
