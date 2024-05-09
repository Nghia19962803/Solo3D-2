using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    private static FXManager _instance;
    public static FXManager Instance { get { return _instance; } }

    [SerializeField] private ParticleSystem explosionFX;
    [SerializeField] private ParticleSystem largeExplosionFX;
    [SerializeField] private ParticleSystem hitFX;
    [SerializeField] private ParticleSystem bulletExplosive;
    [SerializeField] private ParticleSystem dashFX;

    [SerializeField] private ParticleSystem enemyPopup;
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
        // Vector3 pos = new Vector3(trans.position.x, 1f, trans.position.z);
        // Instantiate(hitFX, pos, Quaternion.identity);
    }
    public void BulletExp(Transform trans)
    {
        if (bulletExplosive == null) return;

        Instantiate(bulletExplosive, trans.position, Quaternion.identity);
    }
    public void LargeExplose(Transform trans)
    {
        Vector3 pos = new Vector3(trans.position.x, 0.01f, trans.position.z);
        Instantiate(largeExplosionFX, pos, Quaternion.identity);
    }
    public void Dash(Transform trans)
    {
        Vector3 pos = new Vector3(trans.position.x, 0.01f, trans.position.z);
        Instantiate(dashFX, pos, trans.rotation);
    }
    public void EnemyPopup(Vector3 spawnPos)
    {
        Vector3 pos = new Vector3(spawnPos.x, 0.01f, spawnPos.z);
        Instantiate(enemyPopup, pos, Quaternion.identity);
    }
}
