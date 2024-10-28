using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 1:menu 2:game
    public void EnterGameScene() {
        SceneManager.LoadScene(2);
    }



    public void EndGame() {
        Application.Quit();
    }
}
