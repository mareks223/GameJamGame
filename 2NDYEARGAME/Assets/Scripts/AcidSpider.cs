using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcidSpider : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------------------------------------------------
    Animator anim;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform MainChar;
    public UnityEngine.AI.NavMeshAgent agent;
    bool isPlayerInRange = false;
    public float timeBetweenAttack = 1f;

    public AudioSource acidSpiderAudio;
    public AudioClip ShootingSound;
    public AudioClip spawnSound;



    public GameObject acid;
    public Transform acidSpawnLocation;


    public int spidermaxhp = 100;
    public int Currentspiderhp = 100;

    public Image spiderHpImmage;
   
    bool animisplaying;
    public float bassicattackcd = 4f;
    public float nextattackcd = 0f;

    public float FireBallSpeed = 15;
    void PlayShootingSound()
    {
        acidSpiderAudio.PlayOneShot(ShootingSound);
    }


    //----------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(TryAttack());
        Hpsync();
        anim.Play("moving");
        GameManager.Instance.greenspideramount ++;
        CheckGreenSpiderAmount();
        acidSpiderAudio.PlayOneShot(spawnSound);
    }
    //--------------------------------------------------------BOSS AI--------------------------------------------------------
    IEnumerator TryAttack()
    {
        if (Time.time > nextattackcd && isPlayerInRange && animisplaying == false)
        {
            animisplaying = true;
            StartCoroutine(BasicAttack());
            nextattackcd = Time.time + bassicattackcd; 
        }
        else if(Time.time < nextattackcd && isPlayerInRange && animisplaying == false)
        {
            anim.Play("idle");
        }
        else if(isPlayerInRange == false && (Time.time < nextattackcd) && animisplaying == false)
        {
            anim.Play("moving");
            agent.SetDestination(GameManager.Instance.hero.position);  
        }
        else if(isPlayerInRange == false && animisplaying == false)
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


        if(Currentspiderhp <= 0)
        {
            navMeshAgent.gameObject.SetActive(false);
            GameManager.Instance.greenspideramount -=1;
            MainCharMovement.playerinstance.CurrentPlayerMana += 6;
            MainCharMovement.playerinstance.PlayerHpsync();
            Destroy(gameObject);
        }
        
    }
    void CheckGreenSpiderAmount()
    {
         if(GameManager.Instance.greenspideramount > GameManager.Instance.maxgreenspideramount)
        {
            Destroy(gameObject);
            GameManager.Instance.MobSpawning();
            GameManager.Instance.greenspideramount -=1;
        }

    }


    void Hpsync()
    {
        spiderHpImmage.fillAmount = (float)Currentspiderhp / (float)spidermaxhp;
        
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
        anim.Play("BasicAttack");
    }
    void CreateProjectile()
    {
        var bullet = Instantiate(acid, acidSpawnLocation.position, acidSpawnLocation.rotation);
         bullet.GetComponent<Rigidbody>().velocity = acidSpawnLocation.forward * FireBallSpeed;
    }
   
   
    void NoAnimation()
    {
        animisplaying = false;
        StartCoroutine(AcidSpiderBugFix());
    }
    IEnumerator AcidSpiderBugFix()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(TryAttack());
    }
    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PlayerWeapon":
                Currentspiderhp = Currentspiderhp - 50;
                Hpsync();
                break;
            case  "PlayerSkill1":
                Currentspiderhp = Currentspiderhp - 90;
                Hpsync();
                break;
            case "PlayerSkill2":
                Currentspiderhp = Currentspiderhp - 200;
                Hpsync();
                break;
        }
    }
}
