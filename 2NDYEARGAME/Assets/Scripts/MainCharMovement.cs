using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCharMovement : MonoBehaviour
{
    public Animator animator;

    public int skillOneManaCost;
    public int skillTwoManaCost;
    public int skillThreeManaCost;


    public AudioSource mainCharAudio;
    public AudioClip sweep;
    public AudioClip tpsound;
    public AudioClip thunderstrikesound;
    public AudioClip healsound;

    public GameObject pausescreen;

    public static MainCharMovement playerinstance;

    void PlaySweep()
    {
        mainCharAudio.PlayOneShot(sweep);
    }
    void PlayTp()
    {
        mainCharAudio.PlayOneShot(tpsound);
    }
    void PlayThunder()
    {
        mainCharAudio.PlayOneShot(thunderstrikesound);
    }
    void PlayHeal()
    {
        mainCharAudio.PlayOneShot(healsound);
    }

    



   
    
    public NavMeshAgent agent;
    public Rigidbody player;
    public Collider SwordCollider;
    public float nospeed = 0f;
    bool isattacking;
    public float baseSpeed = 12f;
    //public float bassicattackcd = 0.1f;
    //public float nextattackcd = 0f;


    public Transform transformplayer;



    public int PlayerMaxHp;
    public int CurrentPlayerHp;
    public Image playerHpImage;
    public Text PlayerHpText;

    public int skillTwoHealAmount;



    public int PlayerMaxMana;
    public int CurrentPlayerMana;
    public Image playerManaImage;
    public Text PlayerManaText;





    public float hpregencooldown = 2f;
    public float nexthpregencd = 0;
    public int hpregen;



    public float manaregencooldown = 2f;
    public float nextmanaregencd = 0;
    public int manaregen;






    public GameObject playerskillone;
    public bool playerskilonecoooldown = false;
    public Image skillOneImage;
    public KeyCode ability1;
    public KeyCode pausebutton;

    public GameObject abilitytwo;
    public bool abilityTwoCd = false;
    public Image abilityTwoImage;
    public KeyCode ability2;
    public Transform abilityTwoPoint;

    public bool basicAttackCd = false;
    public Image aAImage;
    public KeyCode basicattack1;






    public GameObject abilitythree;
    public bool abilityThreeCd = false;
    public Image abilityThreeImage;
    public KeyCode ability3;
    public Transform abilityThreePoint;
    

    
    float startTime;



    
    public float skillonecd;
    public float skilltwocd;
    public float skillthreecd;
    public float aACD;

  
    
  
    private float smallestvalue = 0.1f;

    
    
   

  
    void Start()
    {
         skillOneImage.fillAmount = 0;
        aAImage.fillAmount = 0;
        abilityTwoImage.fillAmount = 0;
        abilityThreeImage.fillAmount = 0;
        if(GameManager.Instance.hardmode || GameManager.Instance.hellmode)
        {
            manaregen = manaregen / 2;
            manaregen = hpregen / 2;

            skillonecd = skillonecd * 1.3f;
            skilltwocd = skilltwocd * 1.3f;
        }



        playerinstance = this;
        agent = GetComponent<NavMeshAgent> ();
        animator.SetBool("IsAttacking", isattacking);
        isattacking = false;
    }
    void Update()
    {
        BassicAttack();
        Ability1();
        Ability2();
        Ability3();
        
         if(CurrentPlayerHp <= 0)
        {
            SceneManager.LoadScene("GAME OVER SCENE");
        }
     


        if(CurrentPlayerHp <= PlayerMaxHp)
        {
            if(Time.time > nexthpregencd)
            {
                CurrentPlayerHp = CurrentPlayerHp + hpregen;
                PlayerHpsync();
                nexthpregencd = Time.time + hpregencooldown;
            }
            

        }
        PauseScreen();

        if(CurrentPlayerHp > PlayerMaxHp)
        {
            CurrentPlayerHp = CurrentPlayerHp - (CurrentPlayerHp - PlayerMaxHp);
            PlayerHpsync();
        }
        if(CurrentPlayerMana <= PlayerMaxMana)
        {
            if(Time.time > nextmanaregencd)
            {
                CurrentPlayerMana = CurrentPlayerMana + manaregen;
                PlayerHpsync();
                nextmanaregencd = Time.time + manaregencooldown;
            }
            

        }

        if(CurrentPlayerMana > PlayerMaxMana)
        {
            CurrentPlayerMana = CurrentPlayerMana - (CurrentPlayerMana - PlayerMaxMana);
            PlayerHpsync();
        }



       Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
       if(agent.velocity.magnitude > smallestvalue)
       {
           animator.SetBool("IsWalking", true);
           
       }
       else
       {
           animator.SetBool("IsWalking", false);
           
       }
      
      
         
      
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                agent.SetDestination(hit.point);
            }
        }


        
    }
    void PauseScreen()
    {
         if(Input.GetKey(pausebutton))
        {
            pausescreen.SetActive(true);
            Time.timeScale = 0;
        }
   



    }
    void Ability1()
    {
         if(Input.GetKey(ability1) &&(isattacking == false) &&(playerskilonecoooldown == false) && CurrentPlayerMana >= skillOneManaCost)
        {
            PlayThunder();
            int layer_mask = LayerMask.GetMask("ground");
            playerskilonecoooldown = true;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            animator.Play("SpellcastOne");
            if(Physics.Raycast(ray, out hit, layer_mask))
            {
                Instantiate(playerskillone,hit.point, Quaternion.identity);
                CurrentPlayerMana = CurrentPlayerMana - skillOneManaCost;
                playerskilonecoooldown = true;
                PlayerHpsync();
                skillOneImage.fillAmount = 1;
            }
        }
        if(playerskilonecoooldown)
        {
            skillOneImage.fillAmount -= 1 / skillonecd * Time.deltaTime;
            {
                if(skillOneImage.fillAmount<=0)
                {
                    skillOneImage.fillAmount = 0;
                    playerskilonecoooldown = false;
                }
            }
        }
    }
     void Ability2()
    {
         if(Input.GetKey(ability2) && (abilityTwoCd == false) && CurrentPlayerMana >= skillTwoManaCost)
        {
            PlayHeal();
            int layer_mask = LayerMask.GetMask("ground");
            abilityTwoCd = true;
            CurrentPlayerMana = CurrentPlayerMana - skillTwoManaCost;
            CurrentPlayerHp = CurrentPlayerHp + skillTwoHealAmount;
            Instantiate(abilitytwo, abilityTwoPoint.position, abilityTwoPoint.rotation);     
            PlayerHpsync();
            abilityTwoImage.fillAmount = 1;
           
        }
        if(abilityTwoCd)
        {
            abilityTwoImage.fillAmount -= 1 / skilltwocd * Time.deltaTime;
            {
                if(abilityTwoImage.fillAmount<=0)
                {
                    abilityTwoImage.fillAmount = 0;
                    abilityTwoCd = false;
                }
            }
        }
    }
    void Ability3()
    {
         if(Input.GetKey(ability3) && (isattacking == false) && (abilityThreeCd == false) && CurrentPlayerMana >= skillThreeManaCost)
        {
            PlayTp();
            int layer_mask = LayerMask.GetMask("ground");
            abilityThreeCd = true;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            animator.Play("SpellcastOne");
            if(Physics.Raycast(ray, out hit, layer_mask))
            {
                
                CurrentPlayerMana = CurrentPlayerMana - skillThreeManaCost;
                abilityThreeCd = true;
                Instantiate(abilitythree, abilityThreePoint.position, abilityThreePoint.rotation);
                transformplayer.transform.position = hit.point;
                PlayerHpsync();
                abilityThreeImage.fillAmount = 1;
            }
        }
        if(abilityThreeCd)
        {
            abilityThreeImage.fillAmount -= 1 / skillthreecd * Time.deltaTime;
            {
                if(abilityThreeImage.fillAmount<=0)
                {
                    abilityThreeImage.fillAmount = 0;
                    abilityThreeCd = false;
                }
            }
        }
    }
    void BassicAttack()
    {
        if(Input.GetKey(basicattack1) && (basicAttackCd == false))
        {
            PlaySweep();
            RaycastHit hit;
 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
            if(Physics.Raycast(ray, out hit))
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
            isattacking = true;
            animator.Play("Attack");
            animator.SetBool("IsAttacking", true);
            SwordCollider.enabled = true;
            gameObject.GetComponent<NavMeshAgent>().speed = nospeed;
            agent.ResetPath();
            aAImage.fillAmount = 1;
            basicAttackCd = true;
        }
        if(basicAttackCd)
        {
            aAImage.fillAmount -= 1 / aACD * Time.deltaTime;
            {
                if(aAImage.fillAmount<=0)
                {
                    aAImage.fillAmount = 0;
                    basicAttackCd = false;
                }
            }
        }
       
    }
    void BackToNormal()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = baseSpeed;
        animator.SetBool("IsAttacking", false);
        SwordCollider.enabled = false;
        isattacking = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "SpiderBossWave":
                CurrentPlayerHp = CurrentPlayerHp - 150;
                PlayerHpsync();
                break;
            case "AcidProjectile":
                CurrentPlayerHp = CurrentPlayerHp - 50;
                PlayerHpsync();
                break;
            case "SlamSpiderWave":
                CurrentPlayerHp = CurrentPlayerHp - 100;
                PlayerHpsync();
                break;
            case "BossBasicAttack":
                CurrentPlayerHp = CurrentPlayerHp - 60;
                PlayerHpsync();
                break;
            case "BasicSpiderAttack":
                CurrentPlayerHp = CurrentPlayerHp - 5;
                PlayerHpsync();
                break;
            case "GigaWave":
                CurrentPlayerHp = CurrentPlayerHp - 200;
                PlayerHpsync();
                break;
            case "Anihilation":
                CurrentPlayerHp = CurrentPlayerHp - 400;
                PlayerHpsync();
                break;
           
        }
    }

  





    
    public void PlayerHpsync()
    {
        playerHpImage.fillAmount = (float)CurrentPlayerHp / (float)PlayerMaxHp;
        PlayerHpText.text = PlayerMaxHp.ToString() + " / " + CurrentPlayerHp.ToString();

        playerManaImage.fillAmount = (float)CurrentPlayerMana / (float)PlayerMaxMana;
        PlayerManaText.text = PlayerMaxMana.ToString() + " / " + CurrentPlayerMana.ToString();
    }
}
