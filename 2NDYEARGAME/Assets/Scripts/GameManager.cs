using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> spawnPoints;
    public GameObject ranger;
    public GameObject soldier;
    public GameObject tanker;

    public Transform hero;

    float currentSpawnTime = 0;
    float generationInterval = 5;


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
        //boring
        /*
        SpawnRandomMonster();
        SpawnRandomMonster();
        SpawnRandomMonster();
        SpawnRandomMonster();
        SpawnRandomMonster();
        */

        //simple
        //InvokeRepeating("SpawnRandomMonster", 1f, 2f);

        //paralllel universe
        StartCoroutine(StartSpawningMonsters());        
    }

    IEnumerator StartSpawningMonsters()
    {
        if (currentSpawnTime > generationInterval)
        {
            currentSpawnTime = 0;
            SpawnRandomMonster();
            SpawnRandomMonster();
            yield return new WaitForSeconds(1);
            SpawnRandomMonster();

        }
        yield return null;
        StartCoroutine(StartSpawningMonsters());
    }

    void Update()
    {
        currentSpawnTime += Time.deltaTime;
    }



    void SpawnRandomMonster()
    {
        int randomMonster = Random.Range(0,3);
        int randomPoint = Random.Range(0, spawnPoints.Count); 

        GameObject newParent = spawnPoints[randomPoint];


        GameObject newMonsterPrefab = null;

        if (randomMonster == 0)
        {
            newMonsterPrefab=ranger;
        }
        else if (randomMonster == 1)
        {
            newMonsterPrefab = soldier;
        }
        else if (randomMonster == 2)
        {
            newMonsterPrefab = tanker;
        }




        GameObject monster = Instantiate(newMonsterPrefab);
        monster.transform.position = newParent.transform.position;
        
    }
}
