using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public static MeteorSpawner Instance { get { return s_Instance; } }
    private static MeteorSpawner s_Instance;
    [SerializeField] private GameObject meteorPrefab;
    [Header("Meteor Holder")]
    public List<Transform> pool;

    public bool check;
    private void Awake()
    {
        s_Instance = this;
    }
    public Transform SpawnMeteor()
    {
        //spawn normal bullet by instantiate
        GameObject go = Instantiate(meteorPrefab, transform.position, Quaternion.identity);
        go.transform.SetParent(transform.GetChild(1).transform);    //set meteor parent object by this object
        return go.transform;
    }

    // this method will be called when player tap fire button, and receive player position as parameter
    public void CallMeteor(Vector3 pos)
    {
        Transform meteor = null;
        // check number of bullet in pool
        foreach (Transform t in pool)
        {
            if (t.gameObject.name == meteorPrefab.name + "(Clone)")
            {
                meteor = t;
                meteor.GetComponent<DamageSender>().SendDamage(EnemyBossController.Instance._EnemyRangeStats.GetDmg());   //set damage to meteor = dmg point of boss
                pool.Remove(t);
                meteor.gameObject.SetActive(true);
                meteor.transform.position = new Vector3(pos.x, pos.y + 20, pos.z);
                return;
            }
        }

        //no bullet left in pool => Instantiate a bullet and put in pool
        meteor = SpawnMeteor();
        meteor.GetComponent<DamageSender>().SendDamage(EnemyBossController.Instance._EnemyRangeStats.GetDmg()); //set damage to bullet = player stats class
        meteor.transform.position = new Vector3(pos.x, pos.y + 20, pos.z);
    }
}
