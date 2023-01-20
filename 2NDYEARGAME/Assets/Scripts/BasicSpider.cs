using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicSpider : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------------------------------------------------
    Animator anim;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform MainChar;
    public UnityEngine.AI.NavMeshAgent agent;
    bool isPlayerInRange = false;
    public float timeBetweenAttack = 1f;
    public Collider attackCollider;


    public int spidermaxhp = 100;
    public int Currentspiderhp = 100;

    public Image spiderHpImmage;
   
    bool animisplaying;
    public float bassicattackcd = 4f;
    public float nextattackcd = 0f;



    //----------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        GameManager.Instance.normalspideramount++;
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(TryAttack());
        Hpsync();
        anim.SetBool("IsAnimationPlaying", animisplaying);
    }
    //--------------------------------------------------------BOSS AI--------------------------------------------------------
    IEnumerator TryAttack()
    {
        if (isPlayerInRange && animisplaying == false && Time.time > nextattackcd)
        {
            StartCoroutine(BasicAttack());
            yield return new WaitForSeconds(timeBetweenAttack);
            nextattackcd = Time.time + bassicattackcd;
            
        }
        else if(animisplaying == false)
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
            GameManager.Instance.normalspideramount -=1;
            MainCharMovement.playerinstance.CurrentPlayerMana += 4;
            MainCharMovement.playerinstance.PlayerHpsync();
            Destroy(gameObject);
        }
         
    }
    void CheckBasicSpiderAmount()
    {
         if(GameManager.Instance.normalspideramount > GameManager.Instance.maxnormalspideramount)
        {
            Destroy(gameObject);
            GameManager.Instance.MobSpawning();
            GameManager.Instance.normalspideramount = GameManager.Instance.normalspideramount - 1;
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
        animisplaying = true;
        attackCollider.enabled = true;
         Vector3 direction = GameManager.Instance.hero.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        anim.Play("BasicAttack");
        yield return new WaitForSeconds(2);
        StartCoroutine(BasicAttack());
        
    }
   
   
    void NoAnimation()
    {
        animisplaying = false;
        attackCollider.enabled = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PlayerWeapon":
                Currentspiderhp = Currentspiderhp - 40;
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
    public Collider WaveColliderOne;
   
    
    
    void BeginBasicAttack()
    {
        WaveColliderOne.enabled = true;
   
        StartCoroutine(DiseableColliders());
    }
    IEnumerator DiseableColliders()
    {
        yield return new WaitForSeconds(1);
        WaveColliderOne.enabled = false;
   
    }


}
