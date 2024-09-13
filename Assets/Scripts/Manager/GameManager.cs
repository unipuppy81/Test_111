using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;



public class GameManager : Singleton<GameManager>
{
    [Header("게임 데이터")]
    public ItemData itemData;
    public Player player;

    public static string ENEMY_A = "EnemyA";
    public static string ENEMY_B = "EnemyB";
    public static string COIN = "Coin";
    public static string ARROW = "Arrow";



    [Header("상점 변수")]
    public Dictionary<string, Color> colorDictionary = new Dictionary<string, Color>();
    public Dictionary<string, Image> imageDictionary = new Dictionary<string, Image>();

    [SerializeField] private Image[] iconImageList;


    [Header("게임 로직")]
    public bool isCrush = false;
    public bool bossSpawn = false;
    public float firstBossSpawnTime = 30.0f;
    public float secondBossSpawnTime = 60.0f;
    public float playerMoveTime = 0.0f;


    [SerializeField] private GameObject coinObj;
    [SerializeField] private GameObject[] enemyObj;
    [SerializeField] private GameObject gateObj;
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private float maxSpawnDelay;
    [SerializeField] private float curSpawnDelay;

    [SerializeField] private float gateSpawnTime;
    [SerializeField] private float gateSpawnTimer;




    [Header("보스")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform bossSpwanPos;

    private void Start()
    {
        firstBossSpawnTime = 60.0f;
        playerMoveTime = 0;
        isCrush = false;
        bossSpawn = false;
        gateSpawnTime = 14.0f;
        SpawnEnemyOrCoin();
        SetColor();
    }
    private void Update()
    {
        if (!isCrush && !bossSpawn)
        {
            Debug.Log("보스");
            playerMoveTime += Time.deltaTime;
            gateSpawnTimer += Time.deltaTime;

            if (!IsGateSpawning() && !IsBossSpawning())
            {
                SpawnCoolTime();
            }

            SpawnGateTime();
        }

        if (playerMoveTime >= firstBossSpawnTime && !bossSpawn)
        {
            SpawnBossTime();
        }
    }

    #region 상점
    private void SetColor()
    {
        colorDictionary.Add("Fire", Color.red);
        colorDictionary.Add("Wind", Color.green);
        colorDictionary.Add("None", Color.gray);
    }

    #endregion

    #region 게임 로직
    private void SpawnBossTime()
    {
        if (playerMoveTime >= firstBossSpawnTime && !bossSpawn)
        {
            isCrush = false;
            Instantiate(bossPrefab, bossSpwanPos.position, Quaternion.identity);
            bossSpawn = true;
        }
    }

    private bool IsBossSpawning()
    {
        float timeToBoss = firstBossSpawnTime - playerMoveTime;
        return timeToBoss <= 2f && !bossSpawn;
    }


    private bool IsGateSpawning()
    {
        float timeToNextGate = gateSpawnTime - (gateSpawnTimer % gateSpawnTime);

        return timeToNextGate <= 2f || timeToNextGate >= gateSpawnTime - 2f;
    }

    private void SpawnGateTime()
    {
        if (gateSpawnTimer >= gateSpawnTime)
        {
            Vector3 spawnPosition = new Vector3(0, 4, 0); 
            Instantiate(gateObj, spawnPosition, Quaternion.identity);

            gateSpawnTimer = 0;
        }
    }

    private void SpawnCoolTime()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemyOrCoin();
            maxSpawnDelay = Random.Range(0.5f, 1f);
            curSpawnDelay = 0;
        }
    }

    private void SpawnEnemyOrCoin()
    {
        int spawnCount = Random.Range(0, spawnPos.Length + 1);
        List<Transform> availablePositions = new List<Transform>(spawnPos);

        for (int i = 0; i < spawnCount; i++)
        {
            if (availablePositions.Count == 0)
                break;

            int randomIndex = Random.Range(0, availablePositions.Count);
            float spawnChance = Random.Range(0f, 1f); 

            if (spawnChance <= 0.9f) 
            {
                int ranEnemy = Random.Range(0, enemyObj.Length);

                if(randomIndex == 0){
                    GameObject obj1 = PoolManager.Instance.SpawnFromPool(ENEMY_A, availablePositions[randomIndex].position,
                    availablePositions[randomIndex].rotation);

                    Enemy e = obj1.GetComponent<Enemy>();
                    e.enemyTag = ENEMY_A;
                }
                else
                {
                    GameObject obj2 = PoolManager.Instance.SpawnFromPool(ENEMY_B, availablePositions[randomIndex].position,
                  availablePositions[randomIndex].rotation);

                    Enemy e = obj2.GetComponent<Enemy>();
                    e.enemyTag = ENEMY_B;
                }

              
            }
            else
            {
                GameObject co = PoolManager.Instance.SpawnFromPool(COIN, availablePositions[randomIndex].position,
                    availablePositions[randomIndex].rotation);
            }


            availablePositions.RemoveAt(randomIndex);
        }
    }
    #endregion
}
