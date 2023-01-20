using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject SpiderMob;
    public GameObject Boss;
    public Transform hero;
    public GameObject phaseTwoImage;

    public bool normalmode = false;
    public bool hardmode = false;
    public bool hellmode = false;

    public float extraspeedamount;
    public float extraspeedamountTwo;

    public float extraspeedamounthard;
    public float extraspeedamounthardTwo;

    public AudioSource gamemusic;
    public AudioClip battlechill;
    public AudioClip battlestart;
    public AudioClip battlecontinue;

    public int fuckyouamount;

    bool thistriggeroccuredtwo = false;
    bool lastphaselight = false;

    

    public int normalspideramount;
    public int redspideramount;
    public int greenspideramount;

    public int maxnormalspideramount = 10;
    public int maxredspideramount = 3;
    public int maxgreenspideramount = 7;


    public Light arenalight;
    
    public bool changeLight = false;
   
  
   bool stageone = false;
   bool stagetwo = false;
   bool stagethree = false;
   bool stagefour = false;
   bool laststage = false;

   public float colorspeed = 3f;
   float startTime;

   public bool thistriggeroccured = false;
   int totalspideramount;
   int currentspideramount;

   float minmobrange = 15f;
   float maxmobrange = 25f;

   


   bool stageprozero = false;
   bool stageprocone = false;
   bool stageproctwo = false;
   bool stageprocthree = false;
   bool stageprocfour = false;
   public Color phasetwocolor = new Color(0.3F, 0.15F, 0.15F, 1F);
   public Color endcolor;
   public Color startColor = new Color(0.22F, 0.22F, 0.22F, 1F);

   
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
        totalspideramount = maxgreenspideramount + maxnormalspideramount + maxredspideramount;


        StartCoroutine(MobSpawning());
        startTime = Time.time;


        Color phasetwocolor = new Color(92, 60, 60, 255);
    }
    void Currentspideramount()
    {
        currentspideramount = redspideramount + greenspideramount + normalspideramount;
    }
    void Update()
    { 
        if(!gamemusic.isPlaying && EnemyAttack.enemyattackinstance.bossIsMad)
        {
            gamemusic.clip = battlecontinue;
            gamemusic.Play();
        }

        //=============================== NORMAL MODE ======================================

        if(normalmode)
        {
             if(EnemyAttack.enemyattackinstance.hpPercentage <= 100 && stageprozero == false)
         {
            stageone = true;
            stageprozero = true;
            gamemusic.PlayOneShot(battlechill);
         }
        
          if(EnemyAttack.enemyattackinstance.hpPercentage <= 78 && stageprocone==false)
         {
            stagetwo = true;
            stageprocone=true;
            
            stageone = false;
         }
          if(EnemyAttack.enemyattackinstance.hpPercentage <= 50 && stageproctwo==false)
         {
            stagethree = true;
            stageproctwo=true;
            stagetwo = false;
         }
          if(EnemyAttack.enemyattackinstance.hpPercentage <= 22 && stageprocthree==false)
         {
            EnemyAttack.enemyattackinstance.animisplaying = true;
            minmobrange = 12;
            maxmobrange = 17;
            phaseTwoImage.SetActive(false);
            gamemusic.Stop();
            gamemusic.PlayOneShot(battlestart);
            gamemusic.volume = 0.2f;
            stagefour = true;
            stageprocthree=true;
            EnemyAttack.enemyattackinstance.bossIsMad = true;
            EnemyAttack.enemyattackinstance.BossMaxHp = EnemyAttack.enemyattackinstance.BossMaxHp + fuckyouamount;
            EnemyAttack.enemyattackinstance.CurrrentBossHp = EnemyAttack.enemyattackinstance.BossMaxHp;
            EnemyAttack.enemyattackinstance.ChangeAnimSpeed();
            EnemyAttack.enemyattackinstance.Hpsync();
            EnemyAttack.enemyattackinstance.normalSpeed = EnemyAttack.enemyattackinstance.normalSpeed + extraspeedamount;
            changeLight = true;
            EnemyAttack.enemyattackinstance.anim.Play("Screech");
            stagethree = false;
         }
         if(EnemyAttack.enemyattackinstance.hpPercentage <= 50 && stageprocfour==false && EnemyAttack.enemyattackinstance.bossIsMad)
         {
            minmobrange = 7;
            maxmobrange = 14;
            laststage = true;
            stageprocfour=true;
            EnemyAttack.enemyattackinstance.bossIsGIGAMad = true;
            EnemyAttack.enemyattackinstance.normalSpeed = EnemyAttack.enemyattackinstance.normalSpeed + extraspeedamountTwo;
            EnemyAttack.enemyattackinstance.ChangeAnimSpeedTwo();
            lastphaselight = true;
         }
         if(changeLight && thistriggeroccured == false)
         {
            float t = (Mathf.Sin(Time.time - startTime * colorspeed));
            arenalight.color = Color.Lerp( startColor,phasetwocolor, t);
            if(arenalight.color == phasetwocolor)
            {
                thistriggeroccured = true;
            }
         }
         
         if(lastphaselight && thistriggeroccuredtwo == false)
         {
            float t = (Mathf.Sin(Time.time - startTime * colorspeed));
            arenalight.color = Color.Lerp( phasetwocolor,endcolor, t);
            if(arenalight.color == endcolor)
            {
                thistriggeroccuredtwo = true;
            }
         }
        }
        //=============================== HARD MODE ======================================
         if(hardmode)
        {
             if(EnemyAttack.enemyattackinstance.hpPercentage <= 100 && stageprozero == false)
            {
            stageone = true;
            EnemyAttack.enemyattackinstance.CurrrentBossHp = EnemyAttack.enemyattackinstance.CurrrentBossHp + 1500;
            EnemyAttack.enemyattackinstance.BossMaxHp = EnemyAttack.enemyattackinstance.BossMaxHp + 1500;
            EnemyAttack.enemyattackinstance.Hpsync();
            stageprozero = true;
            gamemusic.PlayOneShot(battlechill);
            }
        
          if(EnemyAttack.enemyattackinstance.hpPercentage <= 90 && stageprocone==false)
            {
            stagetwo = true;
            stageprocone=true;
            
            stageone = false;
            }
          if(EnemyAttack.enemyattackinstance.hpPercentage <= 80 && stageproctwo==false)
            {
            minmobrange = 10;
            maxmobrange = 15;
            stagethree = true;
            stageproctwo=true;
            stagetwo = false;
            }
          if(EnemyAttack.enemyattackinstance.hpPercentage <= 22 && stageprocthree==false)
            {
            EnemyAttack.enemyattackinstance.animisplaying = true;
            minmobrange = 8;
            maxmobrange = 13;
            phaseTwoImage.SetActive(false);
            gamemusic.Stop();
            gamemusic.PlayOneShot(battlestart);
            gamemusic.volume = 0.2f;
            stagefour = true;
            stageprocthree=true;
            EnemyAttack.enemyattackinstance.bossIsMad = true;
            EnemyAttack.enemyattackinstance.BossMaxHp = EnemyAttack.enemyattackinstance.BossMaxHp + fuckyouamount;
            EnemyAttack.enemyattackinstance.CurrrentBossHp = EnemyAttack.enemyattackinstance.BossMaxHp;
            EnemyAttack.enemyattackinstance.ChangeAnimSpeedHardMode();
            EnemyAttack.enemyattackinstance.Hpsync();
            EnemyAttack.enemyattackinstance.normalSpeed = EnemyAttack.enemyattackinstance.normalSpeed + extraspeedamounthard;
            changeLight = true;
            EnemyAttack.enemyattackinstance.anim.Play("Screech");
            stagethree = false;
            }
         if(EnemyAttack.enemyattackinstance.hpPercentage <= 50 && stageprocfour==false && EnemyAttack.enemyattackinstance.bossIsMad)
            {
            minmobrange = 6;
            maxmobrange = 12;
            laststage = true;
            stageprocfour=true;
            EnemyAttack.enemyattackinstance.bossIsGIGAMad = true;
            EnemyAttack.enemyattackinstance.normalSpeed = EnemyAttack.enemyattackinstance.normalSpeed + extraspeedamounthardTwo;
            EnemyAttack.enemyattackinstance.ChangeAnimSpeedHardTwo();
            lastphaselight = true;
            }
         if(changeLight && thistriggeroccured == false)
            {
            float t = (Mathf.Sin(Time.time - startTime * colorspeed));
            arenalight.color = Color.Lerp( startColor,phasetwocolor, t);
            if(arenalight.color == phasetwocolor)
            {
                thistriggeroccured = true;
            }
            }
         
         if(lastphaselight && thistriggeroccuredtwo == false)
            {
                float t = (Mathf.Sin(Time.time - startTime * colorspeed));
                arenalight.color = Color.Lerp( phasetwocolor,endcolor, t);
                if(arenalight.color == endcolor)
                {
                thistriggeroccuredtwo = true;
                }

            }

        
        }    
        //=============================== HELL MODE ======================================
         if(hellmode)
        {
            
          if(EnemyAttack.enemyattackinstance.hpPercentage <= 100 && stageprozero==false)
            {
                stageprozero = true;
                EnemyAttack.enemyattackinstance.CurrrentBossHp = EnemyAttack.enemyattackinstance.CurrrentBossHp + 5000;
                EnemyAttack.enemyattackinstance.BossMaxHp = EnemyAttack.enemyattackinstance.BossMaxHp + 5000;
                EnemyAttack.enemyattackinstance.Hpsync();
                EnemyAttack.enemyattackinstance.animisplaying = true;
                maxredspideramount = maxredspideramount + 2;
                maxgreenspideramount = maxgreenspideramount + 3;
                minmobrange = 8;
                maxmobrange = 13;
                phaseTwoImage.SetActive(false);
                gamemusic.Stop();
                gamemusic.PlayOneShot(battlestart);
                gamemusic.volume = 0.2f;
                stagefour = true;
                stageprocthree=true;
                EnemyAttack.enemyattackinstance.bossIsMad = true;
                EnemyAttack.enemyattackinstance.BossMaxHp = EnemyAttack.enemyattackinstance.BossMaxHp + fuckyouamount;
                EnemyAttack.enemyattackinstance.CurrrentBossHp = EnemyAttack.enemyattackinstance.BossMaxHp;
                EnemyAttack.enemyattackinstance.ChangeAnimSpeedHardMode();
                EnemyAttack.enemyattackinstance.Hpsync();
                EnemyAttack.enemyattackinstance.normalSpeed = EnemyAttack.enemyattackinstance.normalSpeed + extraspeedamounthard;
                changeLight = true;
                EnemyAttack.enemyattackinstance.anim.Play("Screech");
                stagethree = false;
            }
         if(EnemyAttack.enemyattackinstance.hpPercentage <= 78 && stageprocfour==false && EnemyAttack.enemyattackinstance.bossIsMad)
            {
            minmobrange = 5;
            maxmobrange = 11;
            laststage = true;
            stageprocfour=true;
            EnemyAttack.enemyattackinstance.bossIsGIGAMad = true;
            EnemyAttack.enemyattackinstance.normalSpeed = EnemyAttack.enemyattackinstance.normalSpeed + extraspeedamounthardTwo;
            EnemyAttack.enemyattackinstance.ChangeAnimSpeedHardTwo();
            lastphaselight = true;
            }
         if(changeLight && thistriggeroccured == false)
            {
            float t = (Mathf.Sin(Time.time - startTime * colorspeed));
            arenalight.color = Color.Lerp( startColor,phasetwocolor, t);
            if(arenalight.color == phasetwocolor)
            {
                thistriggeroccured = true;
            }
            }
         
         if(lastphaselight && thistriggeroccuredtwo == false)
            {
                float t = (Mathf.Sin(Time.time - startTime * colorspeed));
                arenalight.color = Color.Lerp( phasetwocolor,endcolor, t);
                if(arenalight.color == endcolor)
                {
                thistriggeroccuredtwo = true;
                }

            }

        
        }    
    }
   
    public IEnumerator MobSpawning()
    {
       yield return new WaitForSeconds(Random.Range(minmobrange,maxmobrange));
       if(stageone==true && currentspideramount < totalspideramount)
       {
        SpawnStageOneMobs();
        Currentspideramount();
       }
        if(stagetwo==true && currentspideramount < totalspideramount)
       {
        SpawnStageTwoMobs();
        SpawnStageTwoMobs();
        Currentspideramount();
       }
       if(stagethree==true && currentspideramount < totalspideramount)
       {
        SpawnStageThreeMobs();
        SpawnStageThreeMobs();
        Currentspideramount();
       }
       if(stagefour==true && currentspideramount < totalspideramount)
       {
        SpawnStageThreeMobs();
        SpawnStageThreeMobs();
        SpawnStageThreeMobs();
        Currentspideramount();
       }
      
        StartCoroutine(MobSpawning());
    }
    
    public GameObject[] StageOneMobs;
    public GameObject[] StageTwoMobs;
    public GameObject[] StageThreeMobs;
    public Transform[] SpawnPositions;
    void SpawnStageOneMobs()
    {
        int randomObject = Random.Range(0, StageOneMobs.Length);
        int randomPosition = Random.Range(0, SpawnPositions.Length);
        Instantiate(StageOneMobs[randomObject], SpawnPositions[randomPosition].position, transform.rotation);
    }
    void SpawnStageTwoMobs()
    {
         int randomObject = Random.Range(0, StageTwoMobs.Length);
        int randomPosition = Random.Range(0, SpawnPositions.Length);
        Instantiate(StageTwoMobs[randomObject], SpawnPositions[randomPosition].position, transform.rotation);
    }
    void SpawnStageThreeMobs()
    {
         int randomObject = Random.Range(0, StageThreeMobs.Length);
        int randomPosition = Random.Range(0, SpawnPositions.Length);
        Instantiate(StageThreeMobs[randomObject], SpawnPositions[randomPosition].position, transform.rotation);
    }
}
