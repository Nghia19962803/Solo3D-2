using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHolder : MonoBehaviour
{
    public Slider health;
    public EnemyStats stats;
    public PlayerStats playerStats;
    
    void Start()
    {
        if(transform.gameObject.tag == "Player")
        {
            health.maxValue = playerStats.GetCurrentHealh();
            return;
        }
        health.maxValue = stats.GetCurrentHealh();
    }

    // Update is called once per frame
    void Update()
    {
        health.transform.rotation = Quaternion.Euler(30, 30, 0);

        if (transform.gameObject.tag == "Player")
        {
            health.value = playerStats.GetCurrentHealh();
            return;
        }

        health.value = stats.GetCurrentHealh();
    }
}
