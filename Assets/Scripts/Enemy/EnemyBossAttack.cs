using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAttack : MonoBehaviour
{


    private void Start()
    {

    }
    public void CallOnlyStartGame()
    {
        InvokeRepeating("CallMeteor", 20, 20);
        InvokeRepeating("CallEdoTensei", 30, 10);
    }
    public void ActionWhenAnimationATK()
    {
        CallGreaterMultibleProjectiles();

    }
    public void CallGreaterMultibleProjectiles()
    {
        BossSkill.Instance.SetDmgForObject(EnemyBossController.Instance._EnemyRangeStats.GetDmg());      
        BossSkill.Instance.GreaterMultibleProjectiles(transform);
    }
    public void CallMeteor()
    {
        BossSkill.Instance.SetDmgForObject(EnemyBossController.Instance._EnemyRangeStats.GetDmg());
        BossSkill.Instance.Meteor(EnemyBossController.Instance.GetPlayerPosition());
    }
    public void CallEdoTensei()
    {
        BossSkill.Instance.EdoTensei();
    }
}
