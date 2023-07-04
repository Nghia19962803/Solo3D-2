using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmartVertical : MonoBehaviour
{
    public List<Transform> positionIndex;
    private void Awake() {
        for(int i = 0; i < this.transform.childCount; i ++)
        {
            positionIndex.Add(transform.GetChild(i));
        }
    }
    public void SetTargetPosition(Transform tran)
    {
        this.transform.position = tran.position;
        this.transform.rotation = tran.rotation;
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
    public Transform GetPosition(int index)
    {
        if(index >= positionIndex.Count){
            Debug.Log("not exist transform");
            return null;
        }
        return positionIndex[index];
    }
}
