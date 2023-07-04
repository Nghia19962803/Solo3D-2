using System.Collections.Generic;
using UnityEngine;

public class SmartPool : MonoBehaviour
{
    public static SmartPool singleton;

    [Header("PARTICLE PREFAB")]
    public Transform particleParent;
    public ParticleSystem vfxPrefab;
    public int number1;

    private int count1 = 0;
    private bool spawn1 = true;
    private List<GameObject> particles = new List<GameObject>();

    [Header("GAMEOBJECT PREFAB")]
    public Transform gameObjectParent;
    public GameObject prefab;
    public int number2;

    private int count2 = 0;
    private bool spawn2 = true;
    private List<GameObject> gamePrefab = new List<GameObject>();
    private void Awake()
    {
        singleton = this;
    }
    private void Update()
    {
        if(count1 <= number1 && spawn1)
        {
            GameObject goj = Instantiate(vfxPrefab, particleParent).gameObject;
            particles.Add(goj);
            count1++;
            if(count1 == number1)
            {
                spawn1 = !true;
            }
        }

        if (count2 <= number2 && spawn2)
        {
            GameObject goj = Instantiate(prefab, gameObjectParent);
            gamePrefab.Add(goj);
            count2++;
            if (count2 == number2)
            {
                spawn2 = !true;
            }
        }
    }
    public void PlayParticle()
    {
        GameObject goj = particles[count1-1];
        goj.SetActive(true);
        count1--;
        if(count1 == 0)
        {
            count1 = number1;
        }
    }
    public void PlayPrefab()
    {
        GameObject goj = gamePrefab[count2 - 1];
        goj.SetActive(true);
        count2--;
        if (count2 == 0)
        {
            count2 = number2;
        }
    }
}
