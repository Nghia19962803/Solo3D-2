using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeController : MonoBehaviour
{
    public CubeBossSkill _cubeBeha;

    //
    public LineRenderer lineRender;
    private void Start()
    {
        _cubeBeha.OnMessageReceived += MessageReceive;
        lineRender.positionCount = 2;
    }
    public void MessageReceive(string s)
    {
        switch (s)
        {
            case "moveforward":
                StartCoroutine(MoveForwardAction());
                break;
            case "moveshort":
                StartCoroutine(MoveShort());
                break;
            default:
                break;
        }

    }

    IEnumerator MoveForwardAction()
    {
        bool isBack = false;
        float timer = 0;
        DrawLineRenderer();
        while (!isBack)
        {
            transform.Translate(Vector3.forward * 80 * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                isBack = true;
            }
            yield return null;
        }
        OffLineRenderer();
        transform.DOScale(Vector3.one, 0.1f);
    }
    IEnumerator MoveShort()
    {
        bool isBack = false;
        float timer = 0;
        while (!isBack)
        {
            transform.Translate(Vector3.forward * 10 * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                isBack = true;
            }
            yield return null;
        }
    }
    public void DrawLineRenderer()
    {
        lineRender.SetPosition(0,transform.position);
        lineRender.SetPosition(1,transform.forward * 100);
    }
    public void OffLineRenderer()
    {
        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, transform.position);
    }
}
