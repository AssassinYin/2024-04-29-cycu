using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;

    [SerializeField] private GameObject stopMenu;

    [SerializeField] private GameObject Victory;

    [SerializeField] private GameObject Defeat;

    [SerializeField] private GameObject endMenu;

    float OGTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
        //Instantiate( player, new Vector2(-15,4), Quaternion.identity ); 
        //Instantiate( starTree, new Vector2(40,4), Quaternion.identity );        
    }

    // Update is called once per frame
    void Update()
    {
        checkResult();

        if ( Input.GetKeyDown(KeyCode.Escape)) {
            stopMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if ( Input.GetKeyDown(KeyCode.R)) {
            ResetScene();    
        }

        if ( Time.timeScale == 0f ) {
            openStopMenu();
        }
    }

    private void openStopMenu() {
        if (stopMenu.activeSelf == false ) {
            Time.timeScale = 1f;
        }
        else {
            stopMenu.SetActive(true);
        }
    }

    public void BacktoMenu() {
        SceneManager.LoadScene(1);
    }

    public void ContinueGame() {
        stopMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EnterLevel1() {
        SceneManager.LoadScene(2);
    }
    public void EnterLevel2() {
        SceneManager.LoadScene(3);
    }

    public void EnterLevel3() {
        SceneManager.LoadScene(4);
    }

    public void ResetScene() {
        SceneManager.LoadScene(2);
    }


    private void checkResult() {
        if ( !player.activeSelf ) {
            endMenu.SetActive(true);
            Defeat.SetActive(true);
            
            //defeat
        }
        else if ( !boss.activeSelf ) {
            endMenu.SetActive(true);
            Victory.SetActive(true);
            //victory
        }
    }
}
