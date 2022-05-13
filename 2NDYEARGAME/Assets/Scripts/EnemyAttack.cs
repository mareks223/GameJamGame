using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------------------------------------------------
    Animator anim;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform MainChar;
    public UnityEngine.AI.NavMeshAgent agent;
    bool isPlayerInRange = false;
    public float timeBetweenAttack = 1f;
    private float slamAttackCooldown = 10f;
    private float nextSlamAttackCooldown = 0;
    public GameObject spikes;
    public Transform spikesSpawnLocation;


    //----------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(TryAttack());
    }
    //--------------------------------------------------------BOSS AI--------------------------------------------------------
    IEnumerator TryAttack()
    {
        if (isPlayerInRange)
        {
            BasicAttack();
            yield return new WaitForSeconds(timeBetweenAttack);
        }
        else if(Time.time > nextSlamAttackCooldown)
        {   
            SlamAttack();
            nextSlamAttackCooldown = Time.time + slamAttackCooldown;
            yield return new WaitForSeconds(timeBetweenAttack);
        }
        else
        {
            agent.SetDestination(GameManager.Instance.hero.position);  
        }
        yield return null;
        StartCoroutine(TryAttack());
    }
    //----------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, GameManager.Instance.hero.position);
        if (currentDistance <= navMeshAgent.stoppingDistance + 1)
        {
            isPlayerInRange = true;
            RotateTowards(GameManager.Instance.hero);
        } else
        {
            isPlayerInRange = false;
        }
        anim.SetBool("IsPlayerInRange", isPlayerInRange);
    }
    private void RotateTowards(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
    void BasicAttack()
    {
        anim.Play("BassicAttack");
    }
    void SlamAttack()
    {
        Vector3 direction = MainChar.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        anim.Play("Slam_Attack");
    }
    void CreateSpikes()
    {
        Instantiate(spikes, spikesSpawnLocation.position, spikesSpawnLocation.rotation);     
    }


}
