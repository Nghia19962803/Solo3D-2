using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance { get { return instance; } }

    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject bossPrefabs;

    private int maxX, maxZ;
    public bool spawnGolem;

    [Header("DEBUG ONLY")]
    public Transform[] pos;
    public float delaytime = 1;
    private float count;
    public bool testSpawn { get; set; }

    private void Awake()
    {
        instance = this;
        maxX = 15;
        maxZ = 15;
    }
    private void Update()
    {
        if (testSpawn)
        {
            if(count <= 0)
            {
                TestSpawnEnemy();
                count = delaytime;
            }
            else
            {
                count -= Time.deltaTime;
            }
        }
    }
    public Vector3 SpawnRandomPosition()
    {
        int randomX = Random.Range(-maxX, maxX);
        int randomZ = Random.Range(-maxZ, maxZ);
        return new Vector3(randomX, 0, randomZ);
    }
    public void SpawnEnemy(int index)
    {
        Vector3 spawnPosition = SpawnRandomPosition();
        Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
        FXManager.Instance.EnemyPopup(spawnPosition);
    }
    public void SpawnBoss()
    {
        GameObject goj = Instantiate(bossPrefabs, bossPrefabs.transform.position, Quaternion.identity);
        FXManager.Instance.EnemyPopup(goj.transform.position);
    }

    public void TestSpawnEnemy()
    {
        for (int i = 0; i < pos.Length; i++)
        {
            Instantiate(enemyPrefabs[2], pos[i].position, Quaternion.identity);
        }
        
    }
}
