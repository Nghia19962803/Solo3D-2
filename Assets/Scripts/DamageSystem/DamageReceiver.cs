using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{

    private int dmgReceive;
    public void ReceiveDamage(int dmg)  // method is called by only damage sender class
    {
        dmgReceive = dmg;
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
}