using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foeHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float health;
    void Start()
    {
        health = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
    }

    private void checkHealth() {
        if ( health <= 0 ) {
            Destroy( this );
        }
    }



}
