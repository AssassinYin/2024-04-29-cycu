using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDataWindow : MonoBehaviour
{
    GameObject chooseData;
    // Start is called before the first frame update
    void Start()
    {
        chooseData = GameObject.Find( "Choose Data" );
        chooseData.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void visibleWindow() {
        chooseData.SetActive(true);
    }

    public void unvisibleWindow() {
        chooseData.SetActive(false);
    }
}
