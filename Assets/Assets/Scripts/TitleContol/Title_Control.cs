using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Bottons : MonoBehaviour
{
    
    [SerializeField] GameObject chooseData;
    [SerializeField] GameObject menuBottons;
    private int DataNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        menuBottons.SetActive( true );
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

    public void EnterGame() {
        print( "enter Game" );
        //load data;
        SceneManager.LoadScene( "GameScene" );
    }

    public void chooseData1() { // readData1 & enter Game
        print( "Data1" );
        DataNum = 1;
    }

    public void chooseData2() { // readData2 & enter Game
        print( "Data2" );
        DataNum = 2;
    }

    public void chooseData3() { // readData3 & enter Game
        print( "Data3" );
        DataNum = 3;
    }

    public void visibleWindow() {
        chooseData.SetActive(true);
    }

    public void unvisibleWindow() {
        chooseData.SetActive(false);
    }

}
