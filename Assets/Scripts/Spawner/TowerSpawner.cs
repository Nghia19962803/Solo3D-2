using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    private static TowerSpawner instance;
    public static TowerSpawner Instance { get { return instance; } }
    [SerializeField] private GameObject tower;
    private void Awake()
    {
        instance = this;
    }
    public void SpawnTower(Transform callerTrans)
    {

        Instantiate(tower, new Vector3(callerTrans.position.x, 0, callerTrans.position.z), callerTrans.rotation);
    }
}
