using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangerAtk : MonoBehaviour
{
    private int dmgPoint { get; set; }
    private Transform target;
    public GameObject EnemyBullet;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }
    public void ActiveWhenATK_Animation()
    {
        ReceiveDmgPoint();
    }
    private void FixedUpdate()
    {
        transform.LookAt(target);
    }
    public void ReceiveDmgPoint()
    {
            dmgPoint = EnemyRangerController.Instance._EnemyRangeStats.GetDmg();
            SetDmgToBullet();
    }
    public void SetDmgToBullet()
    {
        //Debug.Log(dmgPoint);
        // goi bullet ra
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GameObject goj = Instantiate(EnemyBullet, pos,transform.rotation);
        // tim class dmg sender roi nhoi dmg stats vao
        goj.GetComponent<DamageSender>().SendDamage(dmgPoint);
        //set bullet active
        goj.SetActive(true);
        // done
    }
}
