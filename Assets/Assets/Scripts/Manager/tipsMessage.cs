using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tipsMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( this.gameObject.activeSelf == true ) {
            if ( Input.GetKeyDown( KeyCode.L ) ) {
                this.gameObject.SetActive(false);
            } 
        }
  
    }


}
