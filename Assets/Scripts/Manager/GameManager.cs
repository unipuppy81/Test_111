using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("상점 변수")]
    public Dictionary<string, Color> colorDictionary = new Dictionary<string, Color>();
    public Dictionary<string, Image> imageDictionary = new Dictionary<string, Image>();

    [SerializeField] private Image[] iconImageList;


    [Header("게임 로직")]
    public bool isCrush = false;
    [SerializeField] private GameObject[] enemyObj;
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private float maxSpawnDelay;
    [SerializeField] private float curSpawnDelay;


    private void Start()
    {
        SpawnEnemy();
        SetColor();
    }
    private void Update()
    {
        if (!isCrush)
        {
            SpawnCoolTime();
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


    private void SpawnCoolTime()
    {
        curSpawnDelay += Time.deltaTime;
        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnEnemy();
            maxSpawnDelay = Random.Range(0.5f, 2f);
            curSpawnDelay = 0;
        }
    }

    private void SpawnEnemy()
    {
        int enemyCount = Random.Range(0, spawnPos.Length + 1);
        List<Transform> availablePositions = new List<Transform>(spawnPos);

        for (int i = 0; i < enemyCount; i++)
        {
            if (availablePositions.Count == 0)
                break;

            int randomIndex = Random.Range(0, availablePositions.Count);
            int ranEnemy = Random.Range(0, enemyObj.Length);

            Instantiate(enemyObj[ranEnemy], availablePositions[randomIndex].position, availablePositions[randomIndex].rotation);
            availablePositions.RemoveAt(randomIndex);
        }
    }
    #endregion
}
