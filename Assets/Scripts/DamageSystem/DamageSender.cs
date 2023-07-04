using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feel;

public class DamageSender : MonoBehaviour
{
    // this class can attach with any object can make damage like bullet, bomb, laser vv...
    // you need to modify damage by call SendDamage method. it shout be called by player stats or enemy stats
    private int damage = 0;

    // this method set damage to your bullet or punch
    private void Start()
    {
        damage = PlayerControllerISO.Instance._stats.GetDmg();
    }
    public int SendDamage(int dmg)
    {
        damage = dmg;
        return damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == gameObject.tag) return; // prevent make dmg for object holding this class and allie who have same tag

        //if (!other.GetComponent<DamageReceiver>()) return;  // just make damage when collider with object which have DamageReceiver class

        //
        if (other.GetComponent<BarbarianEnemy>())
        {
            other.GetComponent<BarbarianEnemy>().TakeDamage(damage);
            other.GetComponent<DamageReceiver>().ReceiveDamage(damage);
        }
        else if (other.GetComponent<DamageReceiver>())
        {
            other.GetComponent<DamageReceiver>().ReceiveDamage(damage);
        }
    }
}
