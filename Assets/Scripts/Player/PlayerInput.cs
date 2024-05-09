﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private AttackController m_AttackController;
    public bool isPlayerControllerInputBlocked { get;set;}

    public Joystick joystick;
    public Joystick gunJoystick;
    public Canvas InputCanvas;

    private Button norAttack;
    private Button specAttack;
    private Button Dash;

    private float dashDelay = 2;
    private float towerDelay = 5;

    private Vector3 m_Movement;
    public Vector3 MoveInput
    {
        get
        {
            if (isPlayerControllerInputBlocked)
            {
                return Vector3.zero;
            }
            return m_Movement;
        }
    }
    public bool isMove
    {
        get { return m_Movement != Vector3.zero;}
    }

    private Vector3 m_faceDir;
    public Vector3 shootInput
    {
        get
        {
            if (isPlayerControllerInputBlocked)
            {
                return Vector3.zero;
            }
            return m_faceDir;
        }
    }
    public bool isShoot;

    public bool IsAttack
    {
        get
        {
            return !isPlayerControllerInputBlocked;
        }
    }
    public bool IsMoveInput { get { return !Mathf.Approximately(MoveInput.magnitude, 0); } }

    public event Action OnShootAction;


    private void Awake()
    {
        m_AttackController = GetComponent<AttackController>();
        //norAttack = InputCanvas.transform.GetChild(1).GetComponent<Button>();
        //specAttack = InputCanvas.transform.GetChild(2).GetComponent<Button>();
        //Dash = InputCanvas.transform.GetChild(4).GetComponent<Button>();
    }
    private void Start()
    {
        // // set nut tấn công thường
        // norAttack.onClick.AddListener(() =>
        // {
        //     CheckNormalAtk();
        // });
        // // set nút tất công đặc biệt
        // specAttack.onClick.AddListener(() =>
        // {
        //     CheckTower();
        // });
        // // set nút dash skill
        // Dash.onClick.AddListener(() =>
        // {
        //     DashSkill();
        // });
        // // set nút uống thuốc hồi máu


    }
    void Update()
    {
        m_faceDir = new Vector3(gunJoystick.Horizontal,0,gunJoystick.Vertical);
        m_Movement.Set(joystick.Horizontal, 0, joystick.Vertical);
        isShoot = m_faceDir.magnitude > 0.2f;
        dashDelay = dashDelay - Time.deltaTime;
        towerDelay = towerDelay - Time.deltaTime;

        if (dashDelay <= 0)
            dashDelay = 0;

        //Dash.transform.GetChild(0).GetComponent<Text>().text = dashDelay.ToString();

        if (towerDelay <= 0)
            towerDelay = 0;

        //specAttack.transform.GetChild(0).GetComponent<Text>().text = towerDelay.ToString();

        if(Input.GetKeyDown(KeyCode.K))
        {
            CheckNormalAtk();
        }

        if(isShoot)
        {
            OnShootAction?.Invoke();
        }
    }
    private void CheckNormalAtk()
    {
        if (IsAttack)
        {
            m_AttackController.NormalAttack();
        }
    }
    private void CheckTower()
    {
        if (IsAttack)
        {
            m_AttackController.PlaceTower();
            towerDelay = 5;
        }

    }
    private void DashSkill()
    {
        m_AttackController.DashAction();
        dashDelay = 2;
    }
}
