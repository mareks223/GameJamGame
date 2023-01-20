using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    //-----------------------------------------------------------------------------------------------------------------------------------
    public Animator anim;
    UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform MainChar;
    public UnityEngine.AI.NavMeshAgent agent;
    bool isPlayerInRange = false;
    public float timeBetweenAttack = 1f;

    public float noSpeed = 0;
    public float normalSpeed = 4;

    public bool AnimationIsPlaying;

    public AudioSource spideraudio;
    public AudioSource screechspideraudio;
    public AudioClip walkingone;
    public AudioClip walkingtwo;
    public AudioClip slamsgrowl;
    
    
    public AudioClip anihilationSound;
    public AudioSource anihilationAudio;

    public AudioClip basicslam;
    public AudioClip theslam;

    public AudioClip partyBegins;



    private float slamAttackCooldown = 10f;
    private float nextSlamAttackCooldown = 0;

    private float gigaslamAttackCooldown = 15f;
    private float nextGigaSlamAttackCooldown = 0;

    private float anihilationAttackCooldown = 30f;
    private float nextanihilationAttackCooldown = 0;



    public GameObject spikes;
     public GameObject anihilationWave;
    public GameObject gigaspikes;
    public Transform spikesSpawnLocation;
    public Transform anihilationSpawnLocation;
    public float hpPercentage;
    


    public int BossMaxHp = 10000;
    public int CurrrentBossHp = 10000;
    public float multiplyamount;
    public float multiplyamountTwo;

    public float multiplyamounthard;
    public float multiplyamounthardTwo;

    public Image BossHpImmage;
    public Text BossHpText;
    public bool animisplaying;
    public static EnemyAttack enemyattackinstance;


    public bool bossIsMad = false;
    public bool bossIsGIGAMad = false;
    public int gigaslams = 0;

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
    
    public void ChangeAnimSpeed()
    {
        anim.speed = anim.speed * multiplyamount;
    }
    public void ChangeAnimSpeedHardMode()
    {
        anim.speed = anim.speed * multiplyamounthard;
    }
     public void ChangeAnimSpeedTwo()
    {
        anim.speed = anim.speed * multiplyamountTwo;
    }
     public void ChangeAnimSpeedHardTwo()
    {
        anim.speed = anim.speed * multiplyamounthardTwo;
    }

    void PlaysoundOne()
    {
        spideraudio.PlayOneShot(walkingone);
    }
    void PlaysoundAnihilation()
    {
        anihilationAudio.PlayOneShot(anihilationSound);
    }
    void PlaysoundTwo()
    {
        spideraudio.PlayOneShot(walkingtwo);
    }
    void PlaysoundSlam()
    {
        spideraudio.PlayOneShot(theslam);
    }
    void PlaysoundBasicSlam()
    {
        spideraudio.PlayOneShot(basicslam);
    }
     void PlaysoundParty()
    {
        screechspideraudio.PlayOneShot(partyBegins);
    }
    void Awake()
    {
        nextSlamAttackCooldown = Time.time + slamAttackCooldown;
    }


    //----------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        enemyattackinstance = this;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(TryAttack());
        Hpsync();
        anim.SetBool("IsAnimationPlaying", animisplaying);

      
    }
    //--------------------------------------------------------BOSS AI--------------------------------------------------------
    IEnumerator TryAttack()
    {
        if(bossIsGIGAMad && Time.time > nextanihilationAttackCooldown && animisplaying == false && isPlayerInRange == true)
        {   
            Anihilation();
            nextanihilationAttackCooldown = Time.time + anihilationAttackCooldown;
            yield return new WaitForSeconds(timeBetweenAttack);
        }
        else if (isPlayerInRange && animisplaying == false)
        {
            BasicAttack();
            yield return new WaitForSeconds(timeBetweenAttack);
            
        }
        else if(Time.time > nextSlamAttackCooldown && bossIsMad == false && isPlayerInRange == false)
        {   
            SlamAttack();
            nextSlamAttackCooldown = Time.time + slamAttackCooldown;
            yield return new WaitForSeconds(timeBetweenAttack);
        }
        else if(bossIsMad && Time.time > nextGigaSlamAttackCooldown && animisplaying == false && isPlayerInRange == false)
        {   
            Gigaslam();
            if(gigaslams >= 4)
            {
                nextGigaSlamAttackCooldown = Time.time + gigaslamAttackCooldown;
                gigaslams = 0;
            }
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
         if(CurrrentBossHp <= 0)
        {
            SceneManager.LoadScene("Victory");
        }

    }
    public void Hpsync()
    {
        BossHpImmage.fillAmount = (float)CurrrentBossHp / (float)BossMaxHp;
        BossHpText.text = BossMaxHp.ToString() + " / " + CurrrentBossHp.ToString();
        hpPercentage = ((float)CurrrentBossHp / (float)BossMaxHp)*100;
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
    void BasicAttack()
    {
         Vector3 direction = MainChar.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        anim.Play("BassicAttack");
        animisplaying = true;
    }
    void SlamAttack()
    {
        Vector3 direction = MainChar.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        spideraudio.PlayOneShot(slamsgrowl);
        anim.Play("Slam_Attack");
        animisplaying = true;
    }
    void Gigaslam()
    {
        Vector3 direction = MainChar.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        anim.Play("Slam_Attack");
        spideraudio.PlayOneShot(slamsgrowl);
        gigaslams++;
        animisplaying = true;
    }
    void Anihilation()
    {
        Vector3 direction = MainChar.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        anim.Play("ANIHILATIONSLAM");
        spideraudio.PlayOneShot(partyBegins);
        animisplaying = true;
    }
    void CreateSpikes()
    {
        Instantiate(spikes, spikesSpawnLocation.position, spikesSpawnLocation.rotation);     
    }
     void CreateGigaSpikes()
    {
        Instantiate(gigaspikes, spikesSpawnLocation.position, spikesSpawnLocation.rotation);     
    }
    void CreateAnihilation()
    {
        Instantiate(anihilationWave, anihilationSpawnLocation.position, anihilationSpawnLocation.rotation);     
    }
    void NoAnimation()
    {
        animisplaying = false;
        StartCoroutine(BugFix());
    }
    IEnumerator BugFix()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(TryAttack());

    }
    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "PlayerWeapon":
                CurrrentBossHp = CurrrentBossHp - 50;
                Hpsync();
                break;
            case  "PlayerSkill1":
                CurrrentBossHp = CurrrentBossHp - 100;
                Hpsync();
                break;
            case "PlayerSkill2":
                CurrrentBossHp = CurrrentBossHp - 200;
                Hpsync();
                break;
        }
    }
    public Collider WaveColliderOne;
   
    
    
    void BeginBasicAttack()
    {
        WaveColliderOne.enabled = true;
   

    }
    void DiseableColliders()
    {

        WaveColliderOne.enabled = false;
   
    }


}
