using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    private DamageReceiver _dmgReceiver;

    [SerializeField] private int baseDmg = 20;
    [SerializeField] private int baseArmor = 10;
    [SerializeField] private int baseHealth = 100;

    private bool isDead = false;

    private int damage = 0;
    private void Awake()
    {
        DamagePoint = baseDmg;
        ArmorPoint = baseArmor;
        currentHealth = baseHealth;
        _dmgReceiver = GetComponent<DamageReceiver>();
    }
    public void SetDamagePoint(ItemObject _item)
    {
        DamagePoint = baseDmg + _item.buff.value;
    }
    public void SetArmorPoint(ItemObject _item)
    {
        ArmorPoint =baseArmor + _item.buff.value;
    }
    public void Regen(ItemObject _item) // call when use bottle item
    {
        currentHealth = currentHealth + _item.buff.value;
        if(currentHealth > baseHealth)
        {
            currentHealth = baseHealth;
        }
        Debug.Log(currentHealth);
    }
    public int GetDmg()
    {
        return DamagePoint;
    }
    public int GetArmor()
    {
        return ArmorPoint;
    }

    //show reality hp
    public int GetCurrentHealh()
    {
        CalHP_AfterReceiveDamage();
        if(currentHealth <= 0 && !isDead)
        {
            isDead = true;
            currentHealth = 0;
            PlayerControllerISO.Instance.PlayerDeath();
            GameManager.instance.StopGame();
        }
        return currentHealth;
    }


    //calculate hp lose when take damage
    public void CalHP_AfterReceiveDamage()
    {
        damage = _dmgReceiver.ReturnDmgReceive();
        if(damage - (ArmorPoint / 10) <= 0)
        {
            return;
        }
        currentHealth -= damage - (ArmorPoint / 10);
        _dmgReceiver.SetDmgReceive();
    }
}
