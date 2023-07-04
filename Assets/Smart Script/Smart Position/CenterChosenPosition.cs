using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CenterChosenPosition : MonoBehaviour
{
    public List<Transform> positionIndex;
    public Transform target;

    private bool isClock;
    private float angle=0;
    private void Awake() 
    {
        for(int i = 0; i < this.transform.childCount; i ++)
        {
            positionIndex.Add(transform.GetChild(i));
        }
    }
    public void SetScale(float scale, float duration)
    {
        transform.DOScale(Vector3.one * 2, duration);
    }
    public void SetRotate(bool isClock, float angle, float duration)
    {
        if(isClock)
        transform.DORotate(Vector3.up * angle, duration);
        else
        transform.DORotate(Vector3.up * -angle, duration);
    }

    public void SetMove(Transform trans, float speed)
    {
        // time = distance/speed
        float time = Vector3.Distance(trans.position,this.transform.position)/speed;
        transform.DOMove(trans.position, time);
    }
    //
    public void SetLookAt(float duration)
    {
        duration = 2;
        transform.DODynamicLookAt(target.position ,duration);
    }

    //set vị trí để đặt object này
    public void SetLocation(Transform trans)
    {
        this.transform.position = new Vector3(trans.position.x, trans.position.y + 1, trans.position.z);
    }

    public void TestSetRotation(){
        isClock = !isClock;
        //SetRotate(isClock ,angle = angle + 90 ,1);
        SetRotate(isClock ,45 ,1);
    }
    public Transform GetPosition(int index)
    {
        if(index >= positionIndex.Count){
            Debug.Log("not exist transform");
            return null;
        }
        return positionIndex[index];
    }
}
