using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private static EnemyController s_Instance;
    public static EnemyController Instance { get { return s_Instance; } }

    protected EnemyStats m_EnemyRangeStats;
    public EnemyStats _EnemyRangeStats { get { return m_EnemyRangeStats; } }

    protected EnemyBossAttack enemyBossAttack;
    public EnemyBossAttack _enemyBossAttack { get { return enemyBossAttack; } }

    protected Transform movePoint;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Collider collider;

    [SerializeField] protected float distanceToStop;
    protected bool isWalk;
    protected bool isAttack;
    protected bool isDeath;
    protected bool isDeathCheck;

    private float timeAtk = 0;
    public float timeDelay;

    public GameObject bottle;
    private void Awake()
    {
        s_Instance = this;
        m_EnemyRangeStats = GetComponent<EnemyStats>();
        enemyBossAttack = GetComponent<EnemyBossAttack>();
        movePoint = FindObjectOfType<PlayerControllerISO>().transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();

        agent.stoppingDistance = distanceToStop;
        isDeath = false;
    }

    private void OnEnable()
    {
        isDeath = false;
        collider.enabled = true;
        m_EnemyRangeStats.ReSetHp();
    }
    private void FixedUpdate()
    {
        // enemy can walk, attack when player trigger gate collision
        if (GameManager.instance.StartGame() == false)
        {
            return;
        }
        if (isDeath)
            return;
        Death();
        Walk();
        Attack();
    }
    public virtual void Walk()
    {
        if (Vector3.Distance(transform.position, movePoint.position) > distanceToStop && !isAttack && !isDeath)
        {
            agent.destination = movePoint.position;
            isWalk = true;
            return;
        }  
        isWalk = false;
        agent.isStopped = true;
    }
    public virtual void Attack()
    {
        if(Vector3.Distance(transform.position, movePoint.position) <= distanceToStop && !isWalk && !isDeath)
        {
            if(timeAtk <= 0)
            {
                animator.SetBool("Attack", true);
                isAttack = true;
                StartCoroutine(PreventMove());  //pretend enemy move 1.5s when attack is action
                timeAtk = timeDelay;
                return;
            }
        }
        animator.SetBool("Attack",false);
        isAttack = false;
    }
    public virtual void Death()
    {
        // check hp loop, make sure enemy is alive or death
        if(_EnemyRangeStats.GetCurrentHealh() <= 0 && !isDeathCheck)
        {
            SoundManager.Instance.MeleeDeahSound();
            animator.SetBool("Die", true);
            isDeath = true;
            isDeathCheck = true;
            agent.isStopped = true;
            StartCoroutine(StartToDeath()); //after 2s when enemy die. perform die animation
        }
    }
    IEnumerator PreventMove()
    {
        yield return new WaitForSeconds(1.5f);
        isAttack = false;
        agent.isStopped = false;
    }
    IEnumerator StartToDeath()
    {
        // spawn bottle after death
        int numb = Random.Range(0, 4);

        collider.enabled = false;
        yield return new WaitForSeconds(3f);
        if (numb == 2)
        {
            Instantiate(bottle, new Vector3(transform.position.x, 1.5f, transform.position.z), Quaternion.identity);
        }
        animator.SetBool("Die", false);
        gameObject.SetActive(false);
    }
    public Transform GetPlayerPosition()
    {
        return movePoint;
    }
}
