using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShowStats : MonoBehaviour
{
    public Text dmgText;
    public Text armorText;
    public Text healthText;

    // private void FixedUpdate()
    // {
    //     dmgText.text = PlayerControllerISO.Instance._stats.GetDmg().ToString();
    //     armorText.text = PlayerControllerISO.Instance._stats.GetArmor().ToString();
    //     healthText.text = PlayerControllerISO.Instance._stats.GetCurrentHealh().ToString();
    // }
}
