using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this class attach bullet and meteos
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 15;

    //void Start()
    //{
    //    StartCoroutine(DeactiveByTime());
    //}
    private void OnEnable()
    {
        StartCoroutine(DeactiveByTime());
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // after 1.5s deactive this bullet
    IEnumerator DeactiveByTime()
    {
        yield return new WaitForSeconds(1f);
        FXManager.Instance.BulletExp(transform);

        //quay ve lai pool
        //BossSkill.Instance.bullets.Add(this.transform);
        //gameObject.SetActive(false);
        //tu dong an

        Destroy(gameObject);
    }

    //this trigger for meteos when collision with ground. just make particle effect
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))
        {
            FXManager.Instance.BulletExp(transform);
            Destroy(gameObject);
        }
    }
}
