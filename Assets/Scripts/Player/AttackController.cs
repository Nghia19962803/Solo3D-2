using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    private float norAtkDelay = 0.2f;   //set delay time between each shoot
    private float towerDelay = 5f;   //set delay time between each shoot
    private float dashDelay = 2;

    [SerializeField] private float attackRadius;
    public LayerMask checkLayer;
    private Transform target;
    private void Update()
    {
        norAtkDelay -= Time.deltaTime;
        towerDelay -= Time.deltaTime;  
        dashDelay -= Time.deltaTime;

        if (norAtkDelay <= 0)
            norAtkDelay = 0;

        if(towerDelay <= 0)
            towerDelay = 0;

        if(dashDelay <= 0)
            dashDelay = 0;
    }
    //normal atk
    public void NormalAttack()
    {
        FindTarget();
        // pretent spam
        if (norAtkDelay <= 0)
        {
            if(target != null)
            {
                transform.LookAt(target.position);
            }
            BulletSpawner.Instance.FireBullet(firePoint);   //spawn bullet
            PlayerControllerISO.Instance.AttackAction();    // animation
            SoundManager.Instance.ImpactBulletSound();      //play gun impact sound
            norAtkDelay = 0.2f; //set time delay for each attack behavious
        }
    }
    private void FindTarget()
    {
        if (Physics.CheckSphere(transform.position, attackRadius, checkLayer))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius, checkLayer);
            if (colliders.Length > 0)
            {
                target = colliders[0].transform;
                return;
            }
            else
                target = null;
        }
    }
    //special atk
    public void SpecialAttack()
    {
        //// pretent spam
        //if (specialAtkDelay <= 0)
        //{

        //}
    }
    public void PlaceTower()
    {
        // pretent spam
        //// pretent spam
        if (towerDelay <= 0)
        {
            TowerSpawner.Instance.SpawnTower(firePoint);
            SoundManager.Instance.PlaceTowerSound();
            towerDelay = 5;
        }


    }
    public void DashAction()
    {
        // pretent spam
        if (dashDelay <= 0)
        {
            //modify speed up in small time
            //then back to old speed
            SoundManager.Instance.DashSound();
            FXManager.Instance.Dash(transform);
            StartCoroutine(SpeedUp());
            dashDelay = 2;
        }
    }
    IEnumerator SpeedUp()
    {
        PlayerControllerISO.Instance.SpeedModify(25);
        yield return new WaitForSeconds(0.2f);
        PlayerControllerISO.Instance.SpeedModify(5);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, attackRadius);
    }
}
