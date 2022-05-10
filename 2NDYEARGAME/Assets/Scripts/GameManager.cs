using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject SpiderMob;
    public GameObject Boss;
    public Transform hero;
    //Creation of singletone
    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
        //DontDestroyOnLoad
    }
    void Start()
    { 
    }

   

   
  



       
    
}
