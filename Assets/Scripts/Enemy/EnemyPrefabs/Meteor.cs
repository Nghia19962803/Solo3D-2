using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private float timeService = 1;
    private LineRenderer lr;
    private void Start()
    {
        timeService = 1;
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, new Vector3(transform.position.x, transform.position.y, transform.position.z));
        lr.SetPosition(1, transform.position + new Vector3(0, -20, 0));
    }
    private void OnEnable()
    {
        timeService = 1;
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0,new Vector3( transform.position.x,transform.position.y,transform.position.z));
        lr.SetPosition(1,transform.position + new Vector3(0,-20,0));
    }
    private void Update()
    {
        DespawnByTime();
    }
    public void DespawnByTime()
    {
        if (timeService < 0)
        {
            ReturnToPool();
        }
        else
            timeService -= Time.deltaTime;
    }
    public void ReturnToPool()
    {
        MeteorSpawner.Instance.pool.Add(this.transform);    // chinh lai thanh MeteorSpawner
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            FXManager.Instance.Explose(transform);
            Destroy(gameObject);
        }
    }
}
