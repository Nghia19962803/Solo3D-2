using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMap : MonoBehaviour
{
    public static TriggerMap instance;
    public bool trigger;
    public ParticleSystem portalFX;
    private void Awake()
    {
        trigger = false;
        instance = this;
        transform.GetComponent<Collider>().enabled = false;
    }
    public bool CheckTriggerMap()
    {
        return trigger;
    }
    public void HideTrigger()
    {
        transform.GetComponent<Collider>().enabled = false;
        portalFX.Stop();
    }
    public void AppearTrigger()
    {
        transform.GetComponent<Collider>().enabled = true;
        portalFX.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            trigger = true;
        }
    }
}
