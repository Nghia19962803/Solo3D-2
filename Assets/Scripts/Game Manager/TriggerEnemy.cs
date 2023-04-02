using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    public static TriggerEnemy instance;
    public bool trigger;
    public ParticleSystem triggerFX;
    private void Awake()
    {
        trigger = false;
        instance = this;
        transform.GetComponent<Collider>().enabled = false;
    }
    public bool CheckTriggerEnemy()
    {
        return trigger;
    }
    public void HideTrigger()
    {
        trigger = false;
        transform.GetComponent<Collider>().enabled = false;
        triggerFX.Stop();
    }
    public void AppearTrigger()
    {
        transform.GetComponent<Collider>().enabled = true;
        triggerFX.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            trigger = true;
        }
    }
}
