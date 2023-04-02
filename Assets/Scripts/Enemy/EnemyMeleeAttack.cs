using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    private int dmgPoint { get;set; }
    private Transform punchArea { get; set; }
    private void Awake()
    {
        punchArea = transform.GetChild(2);  //child number 2 is collider show the attack area
    }
    public void ActiveWhenPunchAnimation()
    {
        ReceiveDmgPoint();
        SoundManager.Instance.MeleePunchSound();
    }
    public void ReceiveDmgPoint()
    {
        dmgPoint = EnemyController.Instance._EnemyRangeStats.GetDmg();
        SetDmgToPunch();
    }
    public void SetDmgToPunch()
    {
        punchArea.gameObject.SetActive(true);
        punchArea.transform.GetComponent<DamageSender>().SendDamage(dmgPoint);
        StartCoroutine(PunchDelay());   // just appear puch collider in 0.2s to prevent damage sender trigger
    }
    IEnumerator PunchDelay()
    {
        yield return new WaitForSeconds(0.2f);
        punchArea.gameObject.SetActive(false);
    }
}
