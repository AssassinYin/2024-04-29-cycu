using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int countCollection = 0;

    public int targetNum;

    public GameObject Close;

    public GameObject Open;

    [SerializeField] GameObject tpGate;
    void Start()
    {
        Close.SetActive( true );
        Open.SetActive( false );
    }

    // Update is called once per frame
    void Update()
    {
        checkCount();
    }

    private void checkCount() {
        if ( countCollection == targetNum ) {
            tpGate.SetActive( true );
            Close.SetActive( false );
            Open.SetActive( true );
        }
    }

    public void countPlusOne() {
        countCollection++;
    }
}
