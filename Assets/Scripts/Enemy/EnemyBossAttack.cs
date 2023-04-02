using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossAttack : MonoBehaviour
{
    private int[] skillNumber = { 0, 1, 2, 3, 4, 5 };
    private int attackNumber = 6;

    public void ActionWhenAnimationATK()
    {
        if (attackNumber == 6)
        {
            attackNumber = 0;
            RandomSkill();
        }
        //make boss random skill to attack
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
    //fire 5 bullets
    public void CallGreaterMultibleProjectiles()                                                        //fire 5 bullets with difference direction
    {
        BossSkill.Instance.LoadBullets();                                                               // make 5 bullets and add them to pool
        BossSkill.Instance.SetDmgForObject(EnemyBossController.Instance._EnemyRangeStats.GetDmg());     //set dmg for each bullet   
        BossSkill.Instance.GreaterMultibleProjectiles(this.transform);                                  // call bossKill class active skill
        SoundManager.Instance.MeleePunchSound();                                                        //sound
    }
    //call a meteor fall in the sky
    public void CallMeteor()
    {
        SoundManager.Instance.CastMeteorSound();
        MeteorSpawner.Instance.CallMeteor(PlayerControllerISO.Instance.GetPlayerPosition());
    }
    #region skill leap slam is active in here
    public void LeapSlam()
    {
        StartCoroutine(FastMove());
    }
    IEnumerator FastMove()
    {
        SoundManager.Instance.DashSound();
        FXManager.Instance.Dash(transform);

        float countTime = 0.5f;
        Vector3 playerPos = PlayerControllerISO.Instance.GetPlayerPosition();
        while (Vector3.Distance(playerPos, transform.position) > 2 || countTime > 0)            //2 condition to enemy stop move when active skill
        {
            transform.position = Vector3.Lerp(transform.position, playerPos, 3 * Time.deltaTime);
            countTime = countTime - Time.deltaTime;
            yield return null;
        }
        //play sound and fx when reach to player
        FXManager.Instance.LargeExplose(transform);
        SoundManager.Instance.LargeExploseSound();
        yield return null;
    }
    #endregion

    // this menthod make boss spam random skill
    public void RandomSkill()
    {
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
