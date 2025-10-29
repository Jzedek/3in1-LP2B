using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script that manage the pause menu.
public class Menu : MonoBehaviour
{
    [SerializeField]
    // The pause menu.
    protected GameObject pauseMenu;
    // The bool that says if we're in the pause menu or not.
    protected bool inPauseMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If space pressed and not in pause menu.
        if (Input.GetKeyDown(KeyCode.Escape) && !inPauseMenu)
        {
            // Entering the pause menu while setting the bool to true.
            inPauseMenu = true;
            EnterPauseMenu();
        }
        // If space pressed and in the pause menu.
        else if (Input.GetKeyDown(KeyCode.Escape) && inPauseMenu)
        {
            // Exiting the pause menu while setting the bool to false.
            inPauseMenu = false;
            ExitPauseMenu();
        }
    }

    void EnterPauseMenu()
    {
        // Set the pause menu to active and stop the time.
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    void ExitPauseMenu()
    {
        // Set the pause menu to not active and resume the time.
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    
}