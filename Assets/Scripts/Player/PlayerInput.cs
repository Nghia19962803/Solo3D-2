using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    private AttackController m_AttackController;
    public bool isPlayerControllerInputBlocked { get;set;}

    public Joystick joystick;
    public Canvas InputCanvas;

    private Button norAttack;
    private Button specAttack;
    private Button Dash;

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

    public bool IsAttack
    {
        get
        {
            return !isPlayerControllerInputBlocked;
        }
    }
    public bool IsMoveInput { get { return !Mathf.Approximately(MoveInput.magnitude, 0); } }
    private void Awake()
    {
        m_AttackController = GetComponent<AttackController>();
        norAttack = InputCanvas.transform.GetChild(1).GetComponent<Button>();
        specAttack = InputCanvas.transform.GetChild(2).GetComponent<Button>();
        Dash = InputCanvas.transform.GetChild(4).GetComponent<Button>();
    }
    private void Start()
    {
        // set nut tấn công thường
        norAttack.onClick.AddListener(() =>
        {
            CheckNormalAtk();
        });
        // set nút tất công đặc biệt
        specAttack.onClick.AddListener(() =>
        {
            CheckTower();
        });
        // set nút dash skill
        Dash.onClick.AddListener(() =>
        {
            DashSkill();
        });
        // set nút uống thuốc hồi máu


    }
    void Update()
    {
        m_Movement.Set(joystick.Horizontal, 0, joystick.Vertical);
    }
    private void CheckNormalAtk()
    {
        if (IsAttack)
        {
            m_AttackController.NormalAttack();
        }
    }
    //private void CheckSpecialAtk()
    //{
    //    if (IsAttack)
    //    {
    //        m_AttackController.SpecialAttack();
    //    }
    //}
    private void CheckTower()
    {
        if (IsAttack)
        {
            m_AttackController.PlaceTower();
        }
    }
    private void DashSkill()
    {
        m_AttackController.DashAction();
    }
}
