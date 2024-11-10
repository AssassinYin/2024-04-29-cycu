using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int countCollection = 0;

    public int targetNum;

    [SerializeField] GameObject tpGate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkCount();
    }

    private void checkCount() {
        if ( countCollection == targetNum ) {
            tpGate.SetActive( true );
        }
    }

    public void countPlusOne() {
        countCollection++;
    }
}
