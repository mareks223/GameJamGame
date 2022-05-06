using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool AnimationIsPlaying;
    float noSpeed = 0f;
    float normalSpeed = 2f;
    void AnimationIsPlayingOn()
    {
        AnimationIsPlaying = true;
    }
    void AnimationIsPlayingOff()
    {
        AnimationIsPlaying = false;
    }
    
    public UnityEngine.AI.NavMeshAgent agent;
   
    void Update()
    {     
        if(AnimationIsPlaying!)
        {
            agent.SetDestination(GameManager.Instance.hero.position);  
            agent.speed = noSpeed;
        }       
        else
        {
            agent.speed = normalSpeed;
        }
    }
}
