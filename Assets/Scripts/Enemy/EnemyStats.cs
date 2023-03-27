using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    private DamageReceiver m_DamageReceiver;

    [SerializeField] private int baseDmg = 2;
    [SerializeField] private int baseArmor = 1;
    [SerializeField] private int baseHealth = 40;

    private int damageTaken;
    private void Awake()
    {
        m_DamageReceiver = GetComponent<DamageReceiver>();
        DamagePoint = baseDmg;
        ArmorPoint = baseArmor;
        currentHealth = baseHealth;
    }
    public void ReSetHp()
    {
        currentHealth = baseHealth;
    }
    public int GetDmg()
    {
        return DamagePoint;
    }

    //get reality health
    public int GetCurrentHealh()
    {
        CalHP_AfterReceiveDamage();
        return currentHealth;
    }

    //calculate damage reality taken when have armor
    public void CalHP_AfterReceiveDamage()
    {
        damageTaken = m_DamageReceiver.ReturnDmgReceive();

        // because enemy's armor too small, I multiplication by 5
        if (damageTaken - (ArmorPoint * 5) <= 0)    
        {
            return;
        }
        currentHealth -= damageTaken - (ArmorPoint * 5);
        m_DamageReceiver.SetDmgReceive();   // after take damage must reset damage taken to prevent lose Hp loop
    }
}
