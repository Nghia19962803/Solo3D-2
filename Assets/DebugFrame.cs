using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugFrame : MonoBehaviour
{
    private bool spawn;

    public TextMeshProUGUI frame;

    //
    private int lastFrameIndex;
    private float[] frameDeltaTimeArray;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        frameDeltaTimeArray = new float[50];
    }
    public void Spawn()
    {
        spawn = !spawn;
        if (spawn)
        {
            EnemySpawner.Instance.testSpawn = true;
        }
        else
        {
            EnemySpawner.Instance.testSpawn = false;
        }
    }
    private void Update()
    {
        frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
        lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;
        frame.text = Mathf.RoundToInt(CalculateFPS()).ToString();
    }
    public float CalculateFPS()
    {
        float total = 0f;
        foreach (float deltaTime in frameDeltaTimeArray)
        {
            total += deltaTime;
        }
        return frameDeltaTimeArray.Length / total;
    }
}
