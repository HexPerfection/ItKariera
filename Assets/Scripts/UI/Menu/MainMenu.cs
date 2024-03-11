using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        LoadSettings.shouldLoadFile = false;
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadFile()
    {
        LoadSettings.shouldLoadFile = true;
        SceneManager.LoadSceneAsync(0);  
    }

    public void Leave()
    {
        Application.Quit();
    }
}
