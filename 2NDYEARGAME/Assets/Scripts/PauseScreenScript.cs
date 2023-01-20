using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenScript : MonoBehaviour
{
    public GameObject pausescreen;
  public void RestartBattle()
    {
        SceneManager.LoadScene("ForestScene");
        Time.timeScale = 1;
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ContinueGame()
    {
        pausescreen.SetActive(false);
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
}
