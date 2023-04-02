using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    // tower atk se cung atk speed voi player
    // dieu kien de tower atk là:
    // 1. enemy nam trong tam ban cua tru, và
    // 2. bị player bắn trúng thì trụ sẻ tấn công phụ 
    [SerializeField] private Transform firePoint;
    [SerializeField] private float attackRadius;
    public LayerMask checkLayer;
    private Transform target;

    [SerializeField] private float delayTime = 1;
    float countTime = 0;
    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }
    private void FixedUpdate()
    {
        FindTarget();
        Attack();
        CheckDistance();
        countTime -= Time.fixedDeltaTime;
        if(countTime < 0) countTime = 0;
    }
    public void Attack()
    {
        if (target == null) return;
        if (countTime <= 0)
        {
            transform.LookAt(target.position);
            BulletSpawner.Instance.FireBullet(firePoint);
            SoundManager.Instance.ImpactBulletSound();      //play gun impact sound

            countTime = delayTime;
            Debug.Log(target.name);
        }
    }
    public void CheckDistance()
    {
        if(target == null) return;
        float distance = Vector3.Distance(target.position, this.transform.position);
        if (distance > attackRadius)
        {
            target = null;
        }
    }
    private void FindTarget()
    {
            if (Physics.CheckSphere(transform.position, attackRadius, checkLayer))
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius, checkLayer);
                if(colliders.Length > 0)
                {
                    target = colliders[0].transform;
                    return;
                }
                else
                    target = null;
            }
    }
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(this.transform.position, attackRadius);
    //}
}
