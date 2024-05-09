using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance { get { return s_Instance; } }
    private static BulletSpawner s_Instance;

    [SerializeField] private List<GameObject> typeOfBlullet;
    [Header("Bullet Holder")]
    public List<Transform> pool;
    private int normalBulletIndex;
    private int specialBulletIndex;
    private int damageModify;
    void Start()
    {
        s_Instance = this;
        normalBulletIndex = 0;
        specialBulletIndex = 1;
        damageModify = 2;
        SpawnBullet();
    }


    public Transform SpawnBullet()
    {
        //spawn normal bullet by instantiate
        GameObject go = Instantiate(typeOfBlullet[normalBulletIndex],transform.position,Quaternion.identity);
        go.transform.SetParent(transform.GetChild(0).transform);    //set bullet parent object by this object
        return go.transform;
    }

    // this method will be called when player tap fire button, and receive player position as parameter
    public void FireBullet(Transform pos)
    {
        Transform bullet = null;
        // check number of bullet in pool
        foreach (Transform t in pool)
        {
            if(t.gameObject.name  == typeOfBlullet[normalBulletIndex].name + "(Clone)")
            {
                bullet = t;
                bullet.GetComponent<DamageSender>().SendDamage(PlayerControllerISO.Instance._stats.GetDmg());   //set damage to bullet = player stats class
                pool.Remove(t);
                bullet.gameObject.SetActive(true);
                bullet.transform.position = pos.position;
                bullet.transform.rotation = pos.rotation;
                return;
            }
        }

        //no bullet left in pool => Instantiate a bullet and put in pool
        bullet = SpawnBullet();
        bullet.GetComponent<DamageSender>().SendDamage(PlayerControllerISO.Instance._stats.GetDmg()); //set damage to bullet = player stats class
        bullet.transform.position = pos.position;
        bullet.transform.rotation = pos.rotation;

        Debug.Log("11111111111");
    }

    // this method will be called when player tap spacial fire button, and receive player position as parameter
    public Transform SpawnSpecBullet()
    {
        GameObject go = Instantiate(typeOfBlullet[specialBulletIndex], transform.position, Quaternion.identity);
        go.transform.SetParent(transform.GetChild(0).transform);
        return go.transform;
    }
    public void FireSpecBullet(Transform pos)
    {
        Transform bullet = null;
        // check number of special bullet in pool
        foreach (Transform t in pool)
        {
            if (t.gameObject.name == typeOfBlullet[specialBulletIndex].name + "(Clone)")
            {
                bullet = t;
                bullet.GetComponent<DamageSender>().SendDamage(PlayerControllerISO.Instance._stats.GetDmg() * damageModify);   //set damage to bullet = player stats class
                pool.Remove(t);
                bullet.gameObject.SetActive(true);
                bullet.transform.position = pos.position;
                bullet.transform.rotation = pos.rotation;
                return;
            }
        }

        //no special bullet left in pool => Instantiate a bullet and put in pool
        bullet = SpawnSpecBullet();
        bullet.GetComponent<DamageSender>().SendDamage(PlayerControllerISO.Instance._stats.GetDmg() * damageModify);   //set damage to bullet = player stats class
        bullet.transform.position = pos.position;
        bullet.transform.rotation = pos.rotation;
    }
}
