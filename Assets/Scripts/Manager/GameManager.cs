using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isCrush = false;

    [SerializeField] private GameObject[] enemyObj;
    [SerializeField] private Transform[] spawnPos;

    [SerializeField] private float maxSpawnDelay;
    [SerializeField] private float curSpawnDelay;

    private void Start()
    {
        SpawnEnemy();
    }
    private void Update()
    {
        if (!isCrush)
        {
            SpawnCoolTime();
        }
    }

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
}
