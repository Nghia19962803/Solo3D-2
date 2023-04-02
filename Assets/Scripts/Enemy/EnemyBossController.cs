using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossController : EnemyController
{
    private void Start()
    {
        m_EnemyRangeStats = GetComponent<EnemyStats>();
        agent = GetComponent<NavMeshAgent>();
        movePoint = FindObjectOfType<PlayerControllerISO>().transform;
        animator = GetComponent<Animator>();

        isDeath = false;
        distanceToStop = 2;
        agent.stoppingDistance = distanceToStop;
    }
    private void FixedUpdate()
    {
        if(GameManager.instance.StartGame() == false)
        {
            animator.SetBool("Attack", false);
            return; 
        }
        Walk();
        Attack();
        Death();
        BossDie();
        if (movePoint == null) return;  // if no player in scene > return. to pretent erro while game playing
        transform.LookAt(movePoint);
    }
    // 1. check raycast to player
    public bool FaceToPlayer()
    {
        Vector3 dir = movePoint.position - transform.position;
        RaycastHit hit;
        Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), dir, out hit, 50);
        if (hit.collider.name == "Player")
        {
            Debug.DrawRay(transform.position + new Vector3(0, 1.5f, 0), dir, Color.red);

            return true;
        }
        else
        {
            Debug.DrawRay(transform.position + new Vector3(0, 1.5f, 0), dir, Color.yellow);
        }
        return false;
    }

    // overrice Walk methor because I were changed walk condition for boss
    public override void Walk()
    {

        if (!isAttack && !isDeath && !FaceToPlayer())
        {

            agent.destination = movePoint.position;
            isWalk = true;

            return;
        }
        isWalk = false;
        agent.isStopped = true;
    }

    // overrice attack methor because I were changed attack condition for boss
    public override void Attack()
    {

        if (!isWalk && !isDeath )
        {
            animator.SetBool("Attack", true);
            isAttack = true;
            return;
        }
        animator.SetBool("Attack", false);
        isAttack = false;
    }
    public void BossDie()
    {
        if (GameManager.instance == null) return;
        if(isDeath)
            GameManager.instance.EndGame();
    }
}
