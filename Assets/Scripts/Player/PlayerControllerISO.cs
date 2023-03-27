using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerISO : MonoBehaviour
{

    private static PlayerControllerISO s_Instance;
    public static PlayerControllerISO Instance { get { return s_Instance; } }

    private PlayerInput playerInput;
    public PlayerInput _PlayerInput { get { return playerInput; } }

    private PlayerStats stats;
    public PlayerStats _stats { get { return stats; } }

    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 5;

    private CharacterController characterController;
    private Animator animator;
    private Vector3 _input;

    // Animator Parameter Hashes
    private readonly int m_HashForwardSpeed = Animator.StringToHash("ForwardSpeed");
    private readonly int m_Attack = Animator.StringToHash("Shoot");
    private readonly int m_HashDeath = Animator.StringToHash("Death");
    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        s_Instance = this;
    }
    private void Update()
    {
        GatherInput();
        Look();
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
    public void AttackAction()
    {
        animator.SetTrigger(m_Attack);
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
}

