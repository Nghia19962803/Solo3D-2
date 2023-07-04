using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;
    public static SkillManager Instance { get { return instance; } }

    public GameObject normalArrow;
    public GameObject fiveArrows;
    public GameObject fireArr;

    public Image skillIconImage;

    private bool isHideSkillPanel;
    public GameObject skillPanel;

    private void Start()
    {
        instance = this;
        isHideSkillPanel = true;
    }
    public void ChooseSkill(string name,Transform trans)
    {
        switch (name)
        {
            case "ban_thuong":
                NormalArrow(trans);
                break;
            case "ban_5_tia":
                SpawnFiveArrowsSkill(trans);
                break;
            case "fireArr":
                FireArrow(trans);
                break;

            default:
                break;
        }
    }
    public void SpawnFiveArrowsSkill(Transform trans)
    {
        Instantiate(fiveArrows, trans.position, trans.rotation);
    }
    public void NormalArrow(Transform trans)
    {
        Instantiate(normalArrow, trans.position, trans.rotation);
    }
    public void FireArrow(Transform trans)
    {
        Instantiate(fireArr, trans.position, trans.rotation);
    }
    public void SetSkillIcon(Sprite spr)
    {
        skillIconImage.sprite = spr;
    }
    public void HideSkillPanel()
    {
        isHideSkillPanel = !isHideSkillPanel;
        if (isHideSkillPanel)
        {
            skillPanel.SetActive(false);
        }
        else
            skillPanel.SetActive(true);
    }
}
