using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulityscreen : MonoBehaviour
{
    public GameObject diffscreen;
    void Awake()
    {
        Time.timeScale = 0;
    }
    public void NormalMode()
    {
        GameManager.Instance.normalmode = true;
        diffscreen.SetActive(false);
        Time.timeScale = 1;
        
    }
  
    public void HardMode()
    {
       GameManager.Instance.hardmode = true;
       diffscreen.SetActive(false);
       Time.timeScale = 1;
    }
    public void HellMode()
    {
       GameManager.Instance.hellmode = true;
       diffscreen.SetActive(false);
       Time.timeScale = 1;
       
    }
}
