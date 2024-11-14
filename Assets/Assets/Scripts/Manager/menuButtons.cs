using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject selectLevel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 0:menu 1:game
    public void EnterGameScene() {
        selectLevel.SetActive( true );
    }

    public void CloseSelect() {
        selectLevel.SetActive( false );
    }

    public void EnterLV1() {
        CloseSelect();
        SceneManager.LoadScene( 1 );
    }

    public void EnterLV2() {
        CloseSelect();
        SceneManager.LoadScene( 2 );
        
    }

    public void EnterLV3() {
        CloseSelect();
        SceneManager.LoadScene( 3 );
    }


    public void EndGame() {
        Application.Quit();
    }
}
