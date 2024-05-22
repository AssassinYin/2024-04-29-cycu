using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Bottons : MonoBehaviour
{
    
    [SerializeField] GameObject chooseData;

    // Start is called before the first frame update
    void Start()
    {
        chooseData.SetActive( false );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameStart() {
        print( "start!!" );
        visibleWindow();
    }

    public void gameSetting() {
        print( "setting!!" );
        
    }

    public void gameLeaving() {
        print( "Leaving!!" );
        
    }

    public void chooseData1() { // readData1 & enter Game
        print( "Data1" );
        SceneManager.LoadScene( "GameScene" );
    }

    public void chooseData2() { // readData2 & enter Game
        print( "Data2" );
        SceneManager.LoadScene( "GameScene" );
    }

    public void chooseData3() { // readData3 & enter Game
        print( "Data3" );
        SceneManager.LoadScene( "GameScene" );
    }

    public void visibleWindow() {
        chooseData.SetActive(true);
    }

    public void unvisibleWindow() {
        chooseData.SetActive(false);
    }

}
