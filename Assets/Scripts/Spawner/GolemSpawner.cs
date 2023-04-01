using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSpawner : MonoBehaviour
{
    private static GolemSpawner instance;
    public static GolemSpawner Instance { get { return instance; } }

    [SerializeField] private GameObject golemPrefabs;
    public bool spawnGolem;
    private void Awake()
    {
        instance = this;
        spawnGolem = false;
    }
    private void FixedUpdate()
    {
        if (spawnGolem)
        {
            Instantiate(golemPrefabs,transform.position, Quaternion.identity);
            spawnGolem = false;
        }
    }
}
