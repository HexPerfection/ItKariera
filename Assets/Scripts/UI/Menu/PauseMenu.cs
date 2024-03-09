using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Canvas pauseMenu;
    public DataPersistenceManager dataPersistenceManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle the visibility of the pause menu canvas
            if (pauseMenu != null)
            {
                pauseMenu.enabled = !pauseMenu.isActiveAndEnabled;
                if (pauseMenu.isActiveAndEnabled)
                {
                    Time.timeScale = 0;
                } else
                {
                    Time.timeScale = 1;
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        LoadSettings.shouldLoadFile = false;
        SceneManager.LoadSceneAsync(0);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void SaveAndQuit()
    {
        dataPersistenceManager.SaveGame();
        Application.Quit();
    }
}
