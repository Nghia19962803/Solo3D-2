using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{

    public EnemyStats enemyStat;

    private void FixedUpdate()
    {
        enemyStat.GetCurrentHealh();
        //Debug.Log(enemyStat.GetCurrentHealh());
    }
}
