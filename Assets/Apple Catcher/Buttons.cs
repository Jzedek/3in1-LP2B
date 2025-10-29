using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script that manage the buttons of the pause menu and the game over screen.
public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // In the scene Furapi Bird or CasseBrick, if espace pressed return to main menu
        if(SceneManager.GetActiveScene().name == "FurapiBird")
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                MainMenuButton();
            }
        }
        if(SceneManager.GetActiveScene().name == "CasseBrick")
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                MainMenuButton();
            }
        }
    }

    public void RetryButton(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}