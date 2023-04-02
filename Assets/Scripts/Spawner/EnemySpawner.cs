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
    private void Awake()
    {
        instance = this;
        maxX = 15;
        maxZ = 15;
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
        Instantiate(bossPrefabs, bossPrefabs.transform.position, Quaternion.identity);
    }
}
