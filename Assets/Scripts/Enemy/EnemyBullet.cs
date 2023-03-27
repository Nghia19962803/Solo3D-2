using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this class attach bullet and meteos
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 15;

    void OnEnable()
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
        yield return new WaitForSeconds(1.5f);
        FXManager.Instance.BulletExp(transform);
        gameObject.SetActive(false);
    }

    //this trigger for meteos when collision with ground. just make particle effect
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            FXManager.Instance.Explose(transform);
        }
    }
}
