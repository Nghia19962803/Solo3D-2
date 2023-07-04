using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeBossSkill : MonoBehaviour
{
    //[Header("debug")]
    //public bool skillOne;
    //public bool skillTwo;
    //public bool skillThree;
    //public bool skillFour;
    //public bool callBack;
    //public bool callBackAround;
    [Header("===    MASK    ===")]
    public List<Transform> masks = new List<Transform>();

    [Header("Root")]
    public Transform FrontPoint;

    //
    private bool isBusy;

    public List<Transform> magicCube = new List<Transform>();

    public SmartVertical smVerticle;
    public CenterChosenPosition ccp;

    public event System.Action<string> OnMessageReceived;
    private int count = 0;
    [Header("velocity cube")]
    public float speed = 60;
    float timer = 2;
    public Transform cubeContainer;

    #region =============   COMBO ===============
    public void ActiveComboOne()
    {
        StartCoroutine(ComboOne());
    }
    public void ActiveComboTwo()
    {
        StartCoroutine(ComboTwo());
    }
    public void ActiveComboThree()
    {
        StartCoroutine(ComboThree());
    }
    public void ActiveComboFour()
    {
        StartCoroutine(ComboFour());
    }
    IEnumerator ComboOne()
    {
        //isChangeState = false;
        SkillOne();
        yield return new WaitUntil(() => !isBusy);
        transform.LookAt(EnemyNewBossController.Instance.GetPlayerTransform());
        StartCoroutine(CallAllCubeBack());
        yield return new WaitUntil(() => !isBusy);
        SkillTwo();  
        yield return new WaitUntil(() => !isBusy);
        EnemyNewBossController.Instance.SetState(EnemyState.Move);
        //isChangeState = true;

    }
    IEnumerator ComboTwo()
    {
        SkillOne();
        yield return new WaitUntil(() => !isBusy);
        SkillTwo();
        yield return new WaitUntil(() => !isBusy);
        StartCoroutine(CallAllCubeBack());
        yield return new WaitUntil(() => !isBusy);
        SkillThree();
        yield return new WaitUntil(() => !isBusy);
        StartCoroutine(CallAllCubeBackAround());   
        yield return new WaitUntil(() => !isBusy);
        EnemyNewBossController.Instance.SetState(EnemyState.Move);
    }
    IEnumerator ComboThree()
    {
        SkillOne();
        yield return new WaitUntil(() => !isBusy);
        StartCoroutine(CallAllCubeBack());
        yield return new WaitUntil(() => !isBusy);
        SkillOne();
        yield return new WaitUntil(() => !isBusy);
        StartCoroutine(CallAllCubeBackAround());
        yield return new WaitUntil(() => !isBusy);
        SkillThree();
        yield return new WaitUntil(() => !isBusy);
        StartCoroutine(CallAllCubeBack());
        yield return new WaitUntil(() => !isBusy);
        EnemyNewBossController.Instance.SetState(EnemyState.Move);
    }
    IEnumerator ComboFour()
    {
        SkillFour();
        yield return new WaitUntil(() => !isBusy);
        transform.LookAt(EnemyNewBossController.Instance.GetPlayerTransform());
        StartCoroutine(CallAllCubeBackAround());
        yield return new WaitUntil(() => !isBusy);
        
        SkillThree();
        yield return new WaitUntil(() => !isBusy);
        StartCoroutine(CallAllCubeBackAround());
        EnemyNewBossController.Instance.SetState(EnemyState.Move);

    }
    #endregion

    #region =============   SKILL 1 ===============
    public void SkillOne()
    {
        transform.LookAt(EnemyNewBossController.Instance.GetPlayerTransform());
        EnemyNewBossController.Instance.AttackAnimate();
        StartCoroutine(ActionSkillOne());
    }
    IEnumerator ActionSkillOne()
    {
        isBusy = true;

        //set SV trước mặt
        smVerticle.SetTargetPosition(FrontPoint);
        //láy tọa độ 3 điểm đầu tiên cho Cube vào đó
        for (int i = 0; i < magicCube.Count; i++)
        {
            masks[i].transform.position = smVerticle.GetPosition(i).position;
            masks[i].transform.rotation = smVerticle.GetPosition(i).rotation;
            //magicCube[i].position = smVerticle.GetPosition(i).position;
            magicCube[i].DOMove(smVerticle.GetPosition(i).position, 0.5f);
            magicCube[i].rotation = smVerticle.GetPosition(i).rotation;
            magicCube[i].DOScaleY(3, 0.5f);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        EnemyNewBossController.Instance.AttackAnimate();
        for (int i = 0; i < masks.Count; i++)
        {
            masks[i].position = masks[i].parent.position;
            yield return null;
        }

        OnMessageReceived?.Invoke("moveforward");
        yield return new WaitForSeconds(1f);
        isBusy = false;
    }
    public void MoveAllCube()
    {
        for (int i = 0; i < magicCube.Count; i++)
        {
            magicCube[i].DOMove(smVerticle.GetPosition(i).position, 1);
        }
    }
    #endregion


    #region =============   SKILL 2 ===============
    public void SkillTwo()
    {
        EnemyNewBossController.Instance.SetState(EnemyState.Look);
        StartCoroutine(MoveOneCube());
    }
    IEnumerator MoveOneCube()
    {
        isBusy = true;
        

        for (int i = 0; i < magicCube.Count; i++)
        {
            SetCenterPoint(EnemyNewBossController.Instance.GetPlayerTransform());
            EnemyNewBossController.Instance.AttackAnimate();
            //v = q/t -> t = q/v
            Vector3 pos = ccp.GetPosition(count).position;
            float timer = Vector3.Distance(pos, magicCube[count].position) / speed;
            masks[i].position = pos;                                //đưa mask đến trước

            magicCube[count].DOMove(pos, timer);                    //lao đến
            yield return new WaitForSeconds(timer);                 //đợi di chuyển
            magicCube[count].DOScale(Vector3.one * 3, 0.3f);        //scale x3
            yield return new WaitForSeconds(0.7f);                  //đợi scale xong
            magicCube[count].DOScale(Vector3.one, 0.5f);            //thu nhỏ lại
            count++;
            if (i == magicCube.Count - 1)
            {
                count = 0;
            }
        }
        
        yield return new WaitForSeconds(0.7f);
        isBusy = false;
    }
    #endregion

    #region =============   SKILL 3 ===============
    public void SkillThree()
    {
        EnemyNewBossController.Instance.SetState(EnemyState.Look);

        StartCoroutine(MoveAndHideOneCube());
    }
    IEnumerator MoveAndHideOneCube()
    {
        isBusy = true;

        

        for (int i = 0; i < magicCube.Count; i++)
        {
            SetCenterPoint(EnemyNewBossController.Instance.GetPlayerTransform());
            //v = q/t
            Vector3 pos = ccp.GetPosition(count).position;
            masks[i].transform.position = ccp.GetPosition(i).position;

            magicCube[count].position = new Vector3(pos.x, pos.y - 3, pos.z);
            EnemyNewBossController.Instance.AttackAnimate();
            yield return new WaitForSeconds(1);
            magicCube[count].localScale = Vector3.one * 3;
            magicCube[count].DOScaleY(10, 0.5f);
            count++;
            if (i == magicCube.Count - 1) count = 0;

            yield return null;
        }
        yield return new WaitForSeconds(1);
        isBusy = false;
    }
    #endregion

    #region =============   SKILL 4 ===============
    public void SkillFour()
    {
        transform.LookAt(EnemyNewBossController.Instance.GetPlayerTransform());

        StartCoroutine(ActionSkil());
    }
    IEnumerator ActionSkil()
    {
        isBusy = true;
        //set SV trước mặt
        smVerticle.SetTargetPosition(FrontPoint);
        //láy tọa độ 3 điểm đầu tiên cho Cube vào đó
        for (int i = 0; i < magicCube.Count; i++)
        {
            //magicCube[i].position = smVerticle.GetPosition(i).position;
            magicCube[i].DOMove(smVerticle.GetPosition(i).position, 0.5f);
            magicCube[i].rotation = smVerticle.GetPosition(i).rotation;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < magicCube.Count; i++)
        {
            OnMessageReceived?.Invoke("moveshort");
            magicCube[i].DOScaleZ(50, 0.5f);
            magicCube[i].SetParent(transform);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        while (timer > 0)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * 180);
            timer -= Time.deltaTime;
            yield return null;
        }
        timer = 2;
        for (int i = 0; i < magicCube.Count; i++)
        {
            magicCube[i].SetParent(cubeContainer);
            
        }
        yield return new WaitForSeconds(0.5f);
        isBusy = false;
    }
    #endregion
    IEnumerator CallAllCubeBack()
    {
        isBusy = true;
        EnemyNewBossController.Instance.AttackAnimate();
        SetVerticalPoint(FrontPoint);
        for (int i = 0; i < magicCube.Count; i++)
        {
            masks[i].transform.position = smVerticle.GetPosition(i).position;
            masks[i].transform.rotation = smVerticle.GetPosition(i).rotation;

            magicCube[i].localScale = Vector3.one;
            magicCube[i].DOMove(smVerticle.GetPosition(i).position, 0.5f);
            magicCube[i].rotation = smVerticle.GetPosition(i).rotation;
        }
        yield return new WaitForSeconds(1f);
        isBusy = false;
    }
    IEnumerator CallAllCubeBackAround()
    {
        isBusy = true;
        EnemyNewBossController.Instance.AttackAnimate();
        SetCenterPoint(this.transform);
        for (int i = 0; i < magicCube.Count; i++)
        {
            masks[i].transform.position = ccp.GetPosition(i).position;

            magicCube[i].localScale = Vector3.one;
            magicCube[i].DOMove(ccp.GetPosition(i).position, 0.5f);
            magicCube[i].rotation = smVerticle.GetPosition(i).rotation;
        }
        yield return new WaitForSeconds(1f);
        isBusy = false;
    }
    public void SetCenterPoint(Transform tran)
    {
        ccp.SetLocation(tran);
    }
    public void SetVerticalPoint(Transform tran)
    {
        smVerticle.SetTargetPosition(tran);
    }
}
//private void Update()
//{
//if (skillOne)
//{
//    skillOne = false;
//    //SkillOne();
//    StartCoroutine(ComboOne());
//}
//if (skillTwo)
//{
//    skillTwo = false;
//    StartCoroutine(ComboTwo());
//}
//if (skillThree)
//{
//    skillThree = false;
//    StartCoroutine(ComboThree());
//}
//if (skillFour)
//{
//    skillFour = false;
//    StartCoroutine(ComboFour());
//}
//if (callBack)
//{
//    callBack = false;
//    CallAllCubeBack();
//}
//if (callBackAround)
//{
//    callBackAround = false;
//    CallAllCubeBackAround();
//}


//}