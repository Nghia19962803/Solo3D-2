using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelection : MonoBehaviour
{
    public string nameSkill;
    public Image icon;

    public void SetSkill()
    {
        PlayerControllerISO.Instance.attack.nameSkill = nameSkill;
        SkillManager.Instance.SetSkillIcon(icon.sprite);
    }
}
