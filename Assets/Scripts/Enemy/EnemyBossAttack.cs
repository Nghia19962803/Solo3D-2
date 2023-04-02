using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAttack : MonoBehaviour
{
    private int[] skillNumber = { 0, 1, 2, 3, 4, 5 };
    private int attackNumber = 6;

    public bool check;
    public void CallOnlyStartGame()
    {
        //InvokeRepeating("CallMeteor", 20, 20);
        //InvokeRepeating("CallEdoTensei", 30, 10);
    }
    public void ActionWhenAnimationATK()
    {
        if (attackNumber == 6)
        {
            attackNumber = 0;
            RandomSkill();
        }
        //chu kỳ sau 6 lần tấn công thì random skill 1 lần
        switch (skillNumber[attackNumber])
        {
            case 0:
            case 1:
            case 2:
                CallGreaterMultibleProjectiles();
                break;
            case 3:
            case 4:
                CallMeteor();
                break;
            case 5:
                LeapSlam();
                break;
        }
        attackNumber++;
    }
    public void CallGreaterMultibleProjectiles()
    {
        BossSkill.Instance.LoadBullets();
        BossSkill.Instance.SetDmgForObject(EnemyBossController.Instance._EnemyRangeStats.GetDmg());      
        BossSkill.Instance.GreaterMultibleProjectiles(this.transform);
        SoundManager.Instance.MeleePunchSound();
    }
    public void CallMeteor()
    {
        //BossSkill.Instance.SetDmgForObject(EnemyBossController.Instance._EnemyRangeStats.GetDmg());
        //BossSkill.Instance.Meteor(EnemyBossController.Instance.GetPlayerPosition());    // nhận ra vị trí player
        SoundManager.Instance.CastMeteorSound();
        MeteorSpawner.Instance.CallMeteor(PlayerControllerISO.Instance.GetPlayerPosition());
    }
    //public void CallEdoTensei()
    //{
    //    BossSkill.Instance.EdoTensei();
    //}
    #region hỗ trợ thực hiện skill Leap Slam
    public void LeapSlam()
    {
        StartCoroutine(FastMove());
    }
    IEnumerator FastMove()
    {
        float countTime = 0.5f;
        Vector3 playerPos = PlayerControllerISO.Instance.GetPlayerPosition();
        SoundManager.Instance.DashSound();
        FXManager.Instance.Dash(transform);
        while (Vector3.Distance(playerPos, transform.position) > 2 || countTime > 0)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos, 3 * Time.deltaTime);
            countTime = countTime - Time.deltaTime;
            yield return null;
        }
        FXManager.Instance.LargeExplose(transform);
        SoundManager.Instance.LargeExploseSound();
        yield return null;
    }
    #endregion

    public void RandomSkill()
    {
        //a,b,tg
        //tg = a
        //a = b
        //b = tg
        int stg;
        for (int i = 0; i < 6; i++)
        {
            int index = Random.Range(0, 5);
            stg = skillNumber[i];
            skillNumber[i] = skillNumber[index];
            skillNumber[index] = stg;
        }
    }
}
