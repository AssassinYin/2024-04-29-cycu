using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class testRead : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public TMP_Text temp;
    [SerializeField] int data;  
    void Start()
    {
        temp.text = "not saved";
        data = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown( KeyCode.S ) ) {

            FileStream store = new FileStream( Application.dataPath + "/save.txt", FileMode.Create ); //可用persistant dataPath
            StreamWriter sw = new StreamWriter( store );
            sw.WriteLine( temp.text );
            sw.WriteLine( data );
            sw.Close();
            store.Close();
            temp.text = "saved";
            data = 100;
            
            

        }

        if ( Input.GetKeyDown( KeyCode.L ) ) {

            FileStream read = new FileStream( Application.dataPath + "/save.txt", FileMode.Open ); //可用persistant dataPath
            StreamReader sr = new StreamReader( read );
            temp.text = sr.ReadLine();
            data = int.Parse( sr.ReadLine() );
            sr.Close();
            read.Close();
            
            
            

        }
    }
}
