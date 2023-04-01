using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangerController : MonoBehaviour
{
    private static EnemyRangerController s_Instance;
    public static EnemyRangerController Instance { get { return s_Instance; } }

    protected EnemyStats m_EnemyRangeStats;
    public EnemyStats _EnemyRangeStats { get { return m_EnemyRangeStats; } }

    protected EnemyBossAttack enemyBossAttack;
    public EnemyBossAttack _enemyBossAttack { get { return enemyBossAttack; } }

    protected Transform movePoint;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Collider collider;

    [SerializeField] protected float distanceToStop;
    protected bool isDeath;

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
        if (GameManager.instance.StartGame() == false)
        {
            return;
        }
        if (isDeath)
            return;
        Death();
        Walk();

        if(timeAtk <= 0)
        {
            Attack();
            timeAtk = timeDelay;
            Debug.Log(timeAtk);
        }


        timeAtk -= Time.fixedDeltaTime;
        if (timeAtk < 0)
            timeAtk = 0;
    }
    public virtual void Walk()
    {
        if (Vector3.Distance(transform.position, movePoint.position) > distanceToStop && !isDeath)
        {
            agent.destination = movePoint.position;
            return;
        }

    }
    public virtual void Attack()
    {
        if (Vector3.Distance(transform.position, movePoint.position) <= distanceToStop && !isDeath)
        {
            animator.SetTrigger("ATK");

            return;
        }

    }
    public virtual void Death()
    {
        // check hp loop, make sure enemy is alive or death
        if (_EnemyRangeStats.GetCurrentHealh() <= 0)
        {
            animator.SetBool("Die", true);
            isDeath = true;
            agent.isStopped = true;
            StartCoroutine(StartToDeath()); //after 2s when enemy die. perform die animation
        }
    }
    IEnumerator StartToDeath()
    {
        int numb = Random.Range(0, 4);


        collider.enabled = false;
        yield return new WaitForSeconds(3f);
        if (numb == 2)
        {
            Instantiate(bottle, new Vector3(transform.position.x, 0.6f, transform.position.z), Quaternion.identity);
        }
        animator.SetBool("Die", false);
        gameObject.SetActive(false);
    }
    public Transform GetPlayerPosition()
    {
        return movePoint;
    }
}
