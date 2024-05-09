
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSMB : SceneLinkedSMB<PlayerControllerISO>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);
        m_MonoBehaviour.playerAttackCtrl.NormalAttack();
        //Debug.Log("state in");
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);
        // m_MonoBehaviour.DelayAttack();
        // m_MonoBehaviour.Idle();
        // Debug.Log("state out");
    }


}