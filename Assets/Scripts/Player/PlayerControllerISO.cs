using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerControllerISO : MonoBehaviour
{

    private static PlayerControllerISO s_Instance;
    public static PlayerControllerISO Instance { get { return s_Instance; } }

    private PlayerInput playerInput;
    public PlayerInput _PlayerInput { get { return playerInput; } }

    private PlayerStats stats;
    public PlayerStats _stats { get { return stats; } }


    private AttackController _attack;
    public AttackController attack { get { return _attack; } }

    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 5;
    [SerializeField] private Transform weaponHolder;
    private CharacterController characterController;
    private Animator animator;
    private Vector3 _input;

    // Animator Parameter Hashes
    private readonly int m_HashForwardSpeed = Animator.StringToHash("ForwardSpeed");
    private readonly int m_Attack = Animator.StringToHash("Shoot");
    private readonly int m_HashDeath = Animator.StringToHash("Death");
    private readonly int m_HashRolling = Animator.StringToHash("Rolling");






    public bool check;
    public ItemObject bow1;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        _attack = GetComponent<AttackController>();
        animator = GetComponent<Animator>();
        s_Instance = this;
    }
    private void Update()
    {
        GatherInput();
        Look();







        if (check)
        {
            check = false;
            //ChangeEquiptWeapon(bow1.prefab);
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        _input = playerInput.MoveInput.normalized;
    }

    // calculate player rotation
    private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed);
    }

    private void Move()
    {
        characterController.Move(transform.forward * _input.sqrMagnitude * Time.deltaTime * _speed);
        animator.SetFloat(m_HashForwardSpeed, _input.sqrMagnitude);
    }
    public void Rolling()
    {
        StartCoroutine(RollingCoroutine());
    }
    IEnumerator RollingCoroutine()
    {
        characterController.Move(transform.forward * _input.sqrMagnitude * Time.deltaTime * _speed);
        animator.SetTrigger(m_HashRolling);
        characterController.detectCollisions = false;
        yield return new WaitForSeconds(5);
        characterController.detectCollisions = true;
    }
    public void AttackAction()
    {
        animator.SetBool(m_Attack,true);
    }
    public void StopAttackAction()
    {
        animator.SetBool(m_Attack,false);
    }
    public void PlayerDeath()
    {
        // ko cho phép di chuyển.
        playerInput.isPlayerControllerInputBlocked = true;
        animator.SetTrigger(m_HashDeath);
    }
    public void SpeedModify(float f)
    {
        _speed = f;
    }
    public Vector3 GetPlayerPosition()
    {
        return transform.position;  
    }
    public Transform GetWeaponHolder()
    {
        if(weaponHolder.childCount > 0)
        {
            Destroy(weaponHolder.GetChild(0).gameObject);
        }
        return weaponHolder;
    }
}

