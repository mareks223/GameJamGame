using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GGScript : MonoBehaviour
{
     public void StartBattle()
    {
        SceneManager.LoadScene("DifficulityScreen");
    }
    public void Tips()
    {

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
