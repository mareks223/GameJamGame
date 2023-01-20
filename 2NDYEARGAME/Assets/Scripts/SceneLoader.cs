using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    public void Restart()
    {
        SceneManager.LoadScene("ForestScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
       SceneManager.LoadScene("MainMenu");
    }

}
