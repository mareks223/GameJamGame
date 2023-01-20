using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool AnimationIsPlaying;
    public static EnemyController enemycontrollerinstance;
    public float noSpeed = 0;
    public float normalSpeed = 4;
    void AnimationIsPlayingOn()
    {
        AnimationIsPlaying = true;
        agent.speed = noSpeed;
    }
    void AnimationIsPlayingOff()
    {
        AnimationIsPlaying = false;
        agent.speed = normalSpeed;
    }
    void start()
    {
        enemycontrollerinstance = this;
    }
    
    public UnityEngine.AI.NavMeshAgent agent;
   
    void Update()
    {     
        if(AnimationIsPlaying!)
        {
            agent.SetDestination(GameManager.Instance.hero.position);  
           
        }       
        
    }
}
