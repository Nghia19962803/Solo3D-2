using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class DamageReceiver : MonoBehaviour
{
    public MMFeedbacks DamageFeedback;
    private int dmgReceive;
    public void ReceiveDamage(int dmg)  // method is called by only damage sender class
    {
        dmgReceive = dmg;
        FXManager.Instance.HitImpact(transform); // appear hit impact when take damage
    }

    // return damage taken for stats class to calculate damage reality taken when have armor
    public int ReturnDmgReceive()
    {
        return dmgReceive;
    }

    //after send damage to stats. make sure damage receiver = 0 to prevent Subtraction hp loop
    public void SetDmgReceive()
    {
        dmgReceive = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DamageFeedback?.PlayFeedbacks(this.transform.position, 123);
        }
    }
}