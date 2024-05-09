using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{

    private int dmgReceive;
    [SerializeField] private GameObject dmgTextDisplay; // text will appear when take damage
    public void ReceiveDamage(int dmg)  // method is called by only damage sender class
    {
        dmgReceive = dmg;
        DisplayText(dmg);   // damage popup

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

    //instantiate a text which how many dmg taken
    public void DisplayText(int dmg)
    {
        GameObject goj = Instantiate(dmgTextDisplay, transform.position, dmgTextDisplay.transform.rotation);
        goj.SetActive(true);
        goj.transform.GetComponent<TextMesh>().text = dmg.ToString();
    }
}