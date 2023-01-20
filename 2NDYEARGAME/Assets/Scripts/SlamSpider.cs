using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class SlamSpider : MonoBehaviour
{

    public Animator anim;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform MainChar;
    public UnityEngine.AI.NavMeshAgent agent;
    bool isPlayerInRange = false;
    public float timeBetweenAttack = 1f;


    public AudioSource slamSpiderAudio;
    public AudioSource slamSpiderGrwolAudio;
    public AudioClip shockwavesound;
    public AudioClip growl;
    public AudioClip awaken;
   


    public int spidermaxhp;
    public int Currentspiderhp;



    public GameObject slamwave;
    public Transform slamwavepoint;

    public float cooldowntime;
    public float nextslamtime;
    public float normalspeed;

    public Image spiderHpImmage;
   
    bool animisplaying;

    void PlaysoundSlamBegin()
    {
        slamSpiderGrwolAudio.PlayOneShot(growl);
    }
     void PlaysoundSlam()
    {
        slamSpiderGrwolAudio.PlayOneShot(shockwavesound);
    }



    //----------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        slamSpiderAudio.PlayOneShot(awaken);
        GameManager.Instance.redspideramount++;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(TryAttack());
        Hpsync();
        animisplaying = false;
        anim.Play("moving");
        CheckRedSpiderAmount();
    }
    //--------------------------------------------------------BOSS AI--------------------------------------------------------
    IEnumerator TryAttack()
    {
        
        if((Time.time > nextslamtime) && isPlayerInRange && animisplaying == false)
        { 
            StartCoroutine(BasicAttack());
            yield return new WaitForSeconds(timeBetweenAttack);
            nextslamtime = Time.time + cooldowntime;
            PlaysoundSlamBegin();
        }
        else if((Time.time < nextslamtime) && isPlayerInRange && animisplaying == false)
        {
            anim.Play("idle");
        }
        else if(isPlayerInRange == false && (Time.time < nextslamtime) && animisplaying == false)
        {
            anim.Play("moving");
            agent.SetDestination(GameManager.Instance.hero.position);  
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
        }
         else
        {
            isPlayerInRange = false;
        }
        anim.SetBool("IsPlayerInRange", isPlayerInRange);
        if(Currentspiderhp <= 0)
        {
            navMeshAgent.gameObject.SetActive(false);
            GameManager.Instance.redspideramount = GameManager.Instance.redspideramount - 1;
            Destroy(gameObject);
        }
        if(animisplaying)
        {
            agent.speed = 0f;
            MainCharMovement.playerinstance.CurrentPlayerMana += 8;
            MainCharMovement.playerinstance.PlayerHpsync();
        }
        else
        {
            agent.speed = normalspeed;
        }
         
    }
    void CheckRedSpiderAmount()
    {
        if(GameManager.Instance.redspideramount > GameManager.Instance.maxredspideramount)
        {
            Destroy(gameObject);
            GameManager.Instance.MobSpawning();
            GameManager.Instance.redspideramount = GameManager.Instance.redspideramount - 1;
        }
    }

    void Hpsync()
    {
        spiderHpImmage.fillAmount = (float)Currentspiderhp / (float)spidermaxhp;
    }


    void CreateSpikes()
    {
        Instantiate(slamwave, slamwavepoint.position, slamwavepoint.rotation);     
    }




    private void RotateTowards(Transform player)
    {
        if(animisplaying == false)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
             transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }
      
    }
    IEnumerator BasicAttack()
    {
        yield return new WaitForSeconds(1);
        Vector3 direction = GameManager.Instance.hero.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        anim.Play("Slam_Attack");
        StartCoroutine(TryAttack());
    }
    void AnimationPlay()
    {
        animisplaying = true;
    }
    void NoAnimation()
    {
        animisplaying = false;
        anim.Play("moving");
    }
   
    public void OnTriggerEnter(Collider other)
    {
        if(GameManager.Instance.hardmode || GameManager.Instance.hellmode)
        {
                switch (other.tag)
            {
                case "PlayerWeapon":
                    Currentspiderhp = Currentspiderhp - 50;
                    Hpsync();
                    break;
                case  "PlayerSkill1":
                    Currentspiderhp = Currentspiderhp - 100;
                    Hpsync();
                    break;
                case "PlayerSkill2":
                    Currentspiderhp = Currentspiderhp - 200;
                    Hpsync();
                    break;
            }
        }
        if(GameManager.Instance.normalmode)
        {
                switch (other.tag)
            {
                case "PlayerWeapon":
                    Currentspiderhp = Currentspiderhp - 75;
                    Hpsync();
                    break;
                case  "PlayerSkill1":
                    Currentspiderhp = Currentspiderhp - 125;
                    Hpsync();
                    break;
                case "PlayerSkill2":
                    Currentspiderhp = Currentspiderhp - 200;
                    Hpsync();
                    break;
            }
        }
    }
}
