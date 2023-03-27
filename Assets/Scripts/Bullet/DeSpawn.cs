using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawn : MonoBehaviour
{
    private float timeService = 1;
    private void OnEnable()
    {
        timeService = 1;
    }
    private void Update()
    {
        DespawnByTime();
    }
    public void DespawnByTime()
    {
        if(timeService < 0)
        {
            ReturnToPool();
        }
        else
            timeService -= Time.deltaTime;
    }
    public void ReturnToPool()
    {
        BulletSpawner.Instance.pool.Add(this.transform);
        gameObject.SetActive(false);
    }
}
