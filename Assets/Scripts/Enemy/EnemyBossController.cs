using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossController : EnemyController
{

    private void Start()
    {
        isDeath = false;
        m_EnemyRangeStats = GetComponent<EnemyStats>();
        distanceToStop = 2;
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = distanceToStop;
        movePoint = FindObjectOfType<PlayerControllerISO>().transform;
        animator = GetComponent<Animator>();

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

        if (!isWalk && !isDeath && FaceToPlayer())
        {
            animator.SetBool("Attack", true);
            isAttack = true;
            //StopCoroutine(PreventMove());
            StartCoroutine(PreventMove());  //hàm làm cho enemy đứng yên khi thực hiện animation attack
            return;
        }
        animator.SetBool("Attack", false);
        isAttack = false;
    }
    public void BossDie()
    {
        if(isDeath)
            GameManager.instance.EndGame();
    }
    IEnumerator PreventMove()
    {
        agent.destination = transform.position;
        yield return new WaitForSeconds(3f);
        isAttack = false;
        agent.isStopped = false;
    }

}
