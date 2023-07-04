using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using MoreMountains.Feel;

public class EnemyNewBossController : MonoBehaviour
{
    private static EnemyNewBossController s_Instance;
    public static EnemyNewBossController Instance { get { return s_Instance; } }

    protected EnemyStats m_EnemyRangeStats;
    public EnemyStats _EnemyRangeStats { get { return m_EnemyRangeStats; } }

    public CubeBossSkill bossSkill;

    protected Transform playerTarget;
    protected Transform movePoint;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Collider collider;

    protected bool isDeath;
    protected bool isDeathCheck;

    [Header("NEW CONTROL LOGIC")]
    private EnemyState currentState;
    public bool attack;
    public bool move;
    public BarbarianEnemy BE_Attack;
    private int comboNumber = 1;

    public Transform lineOne;
    public Transform lineTwo;
    public Transform lineThree;
    public Transform lineFour;

    private int currentLine;
    private void Awake()
    {
        s_Instance = this;
        m_EnemyRangeStats = GetComponent<EnemyStats>();
        playerTarget = FindObjectOfType<PlayerControllerISO>().transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();

        isDeath = false;
    }

    private void OnEnable()
    {
        isDeath = false;
        collider.enabled = true;
        m_EnemyRangeStats.ReSetHp();
    }
    private void Start()
    {
        movePoint = lineOne;
        currentLine = 1;
        SetState(EnemyState.Idle);
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.StartGame() == false)
        {
            return;
        }
        if (isDeath)
            return;


        switch (currentState)
        {
            case EnemyState.Idle:
                //
                //animator.SetBool("Run", false);
                break;

            case EnemyState.Move:
                
                Walk();
                SetState(EnemyState.Idle);
                break;
            case EnemyState.Look:
                transform.LookAt(playerTarget.position);
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Combo1:
                bossSkill.ActiveComboOne();
                SetState(EnemyState.Attack);
                break;
            case EnemyState.Combo2:
                bossSkill.ActiveComboTwo();
                SetState(EnemyState.Attack);
                break;
            case EnemyState.Combo3:
                bossSkill.ActiveComboThree();
                SetState(EnemyState.Attack);
                break;
            case EnemyState.Combo4:
                bossSkill.ActiveComboFour();
                SetState(EnemyState.Attack);
                break;
        }
        Death();
    }
    public virtual void Walk()
    {
        if (!isDeath)
        {
            //movePoint = GetPlayerTransform();
            agent.isStopped = false;
            //agent.destination = 
            //Debug.Log("======= " + movePoint.position);
        }
        agent.isStopped = false;
        StartCoroutine(WalkCoroutine());    
    }
    IEnumerator WalkCoroutine()
    {
        float timer = 2;
        SetState(EnemyState.Idle);

        agent.SetDestination(movePoint.position);
        animator.SetBool("Run", true);
        while (Vector3.Distance(movePoint.position, transform.position) > 0.1f && timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        GetRandomNumer(currentLine);
        animator.SetBool("Run", false);
        agent.isStopped = true;

        switch (comboNumber)
        {
            case 1:
                SetState(EnemyState.Combo1);
                comboNumber++;
                break;
            case 2:
                SetState(EnemyState.Combo2);
                comboNumber++;
                break;
            case 3:
                SetState(EnemyState.Combo3);
                comboNumber = 1;
                break;
            case 4:
                SetState(EnemyState.Combo4);
                comboNumber = 1;
                break;

            default:
                break;
        }
        
    }
    public virtual void Attack()
    {
        if (!isDeath)
        {
            //animator.SetBool("Attack", true);
        }
        SetState(EnemyState.Idle);
    }
    public void AttackAnimate()
    {
        animator.SetBool("Attack", true);
        BE_Attack.AttackEvent();
    }
    public virtual void Death()
    {
        // check hp loop, make sure enemy is alive or death
        if (_EnemyRangeStats.GetCurrentHealh() <= 0 && !isDeathCheck)
        {
            SoundManager.Instance.MeleeDeahSound();
            animator.SetBool("Die", true);
            isDeath = true;
            isDeathCheck = true;
            agent.isStopped = true;
            StartCoroutine(StartToDeath()); //after 2s when enemy die. perform die animation

            PlayerControllerISO.Instance.attack.SetTargetDeath();
        }
    }

    IEnumerator StartToDeath()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(3f);

        animator.SetBool("Die", false);
        gameObject.SetActive(false);
    }

    public void SetState(EnemyState state)
    {
        currentState = state;
    }
    public Transform GetPlayerTransform()
    {
        return playerTarget;
    }
    public void OnDebug()
    {
        SetState(EnemyState.Move);
    }
    public void GetRandomNumer(int lineID)
    {
        int[] line = new int[3];
        int choose;
        int num;
        switch (lineID)
        {
            case 1:
                line[0] = 2;
                line[1] = 3;
                line[2] = 4;
                choose = line[Random.Range(0, 3)];
                num = Random.Range(0, 5);
                currentLine = choose;
                GetMovePoint(choose, num);
                break;
            case 2:
                line[0] = 1;
                line[1] = 2;
                line[2] = 4;
                choose = line[Random.Range(0, 3)];
                num = Random.Range(0, 5);
                currentLine = choose;
                GetMovePoint(choose, num);
                break;
            case 3:
                line[0] = 1;
                line[1] = 2;
                line[2] = 4;
                choose = line[Random.Range(0, 3)];
                num = Random.Range(0, 5);
                currentLine = choose;
                GetMovePoint(choose, num);
                break;
            case 4:
                line[0] = 1;
                line[1] = 2;
                line[2] = 3;
                choose = line[Random.Range(0, 3)];
                num = Random.Range(0, 5);
                currentLine = choose;
                GetMovePoint(choose, num);
                break;
        }
    }
    public void GetMovePoint(int lineNumber, int pointNumber)
    {
        switch (lineNumber)
        {
            case 0:
                movePoint = lineOne.GetChild(pointNumber).transform;
                break;
            case 1:
                movePoint = lineTwo.GetChild(pointNumber).transform;
                break;
            case 2:
                movePoint = lineThree.GetChild(pointNumber).transform;
                break;
            case 3:
                movePoint = lineFour.GetChild(pointNumber).transform;
                break;
        }
        Debug.Log("LINE : " + lineNumber + " | NUMBER : " + pointNumber);
    }
}
public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Combo1,
    Combo2,
    Combo3,
    Combo4,
    Look
}