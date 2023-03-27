using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    // đầu tiên là class con stats sẻ truyền giá trị hp của nó vào đay
    //sau đó class con stats sẻ liên tục đọc giá trị current hp của class này để cập nhật tình trạng cho stats
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