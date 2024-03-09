using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public void Restart()
    {
        LoadSettings.shouldLoadFile = false;
        SceneManager.LoadSceneAsync(0); 
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Quit()
    {
        // Add save functionality
        Application.Quit();
    }
}
