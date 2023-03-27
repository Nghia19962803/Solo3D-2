using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    private static FXManager _instance;
    public static FXManager Instance { get { return _instance; } }

    public ParticleSystem explosionFX;
    public ParticleSystem hitFX;
    public ParticleSystem bulletExplosive;
    private void Awake()
    {
        _instance = this;
    }
    public void Explose(Transform trans)
    {
        Vector3 pos = new Vector3(trans.position.x, 0.01f, trans.position.z);
        Instantiate(explosionFX, pos, Quaternion.identity);
    }
    public void HitImpact(Transform trans)
    {
        Vector3 pos = new Vector3(trans.position.x, 1f, trans.position.z);
        Instantiate(hitFX, pos, Quaternion.identity);
    }
    public void BulletExp(Transform trans)
    {
        if (bulletExplosive == null) return;

        Instantiate(bulletExplosive, trans.position, Quaternion.identity);
    }
}
