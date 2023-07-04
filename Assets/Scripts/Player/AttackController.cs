using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feel;

public class AttackController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;

    private float norAtkDelay = 1f;   //set delay time between each shoot
    private float towerDelay = 5f;   //set delay time between each shoot
    private float dashDelay = 2;

    [SerializeField] private float attackRadius;
    public LayerMask checkLayer;
    private Transform target;

    public BarbarianEnemy BE;


    [Header("debug")]
    public bool isTarget;

    public string nameSkill { get; set; }
    private void Start()
    {
        nameSkill = "ban_thuong";
    }
    public void CheckTarget()
    {
        if(target != null)
            isTarget = true;
        else
            isTarget = false;
    }
    private void Update()
    {
        CheckTarget();
        norAtkDelay -= Time.deltaTime;
        towerDelay -= Time.deltaTime;
        dashDelay -= Time.deltaTime;

        if (norAtkDelay <= 0)
            norAtkDelay = 0;

        if (towerDelay <= 0)
            towerDelay = 0;

        if (dashDelay <= 0)
            dashDelay = 0;

        //AutoAttack();

        if (!PlayerControllerISO.Instance._PlayerInput.IsMoveInput)
        {
            CheckEnemyInRadius();
            if (target != null)
            {
                if (CheckDistanceBetweenPlayerAndEnemy() > 0 && CheckDistanceBetweenPlayerAndEnemy() < attackRadius)
                {
                    NormalAttack();
                }
                else
                {
                    target = null;
                }
            }
        }
        else
        {
            PlayerControllerISO.Instance.StopAttackAction();
        }
        
        if(target == null)
        {
            PlayerControllerISO.Instance.StopAttackAction();
        }
    }
    
    public void ActionWhenAttackAnimation()
    {

        BE.AttackEvent();
        //BulletSpawner.Instance.FireBullet(firePoint);   //spawn bullet
        SkillManager.Instance.ChooseSkill(nameSkill, firePoint);

    }
    //normal atk
    public void NormalAttack()
    {
        if (target != null) transform.LookAt(target.position);
        PlayerControllerISO.Instance.AttackAction();
    }
    public void SetTargetDeath()
    {
        target = null;
        Debug.Log("set new target");
    }
    public void CheckEnemyInRadius()
    {
        if (target != null) return;

        if (Physics.CheckSphere(transform.position, attackRadius, checkLayer))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, attackRadius, checkLayer);
            if (colliders.Length > 0)
            {
                target = colliders[0].transform;
            }
            else
            {
                target = null;
            }
        }
        else
            target = null;
    }

    public float CheckDistanceBetweenPlayerAndEnemy()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            return distance;
        }
        else
        {
            return -1;
        }
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
            //SoundManager.Instance.DashSound();
            FXManager.Instance.Dash(transform);
            PlayerControllerISO.Instance.Rolling();
            dashDelay = 2;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, attackRadius);
    }
}
