using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource shortAudioSource;
    private AudioSource longAudioSource;

    // sound for atk action
    [SerializeField] private AudioClip impactBullet;
    [SerializeField] private AudioClip meleePunch;

    //sound when death
    [SerializeField] private AudioClip enemyMeleeDeath;
    [SerializeField] private AudioClip enemyRangerDeath;

    // sound skill
    [SerializeField] private AudioClip dashSkill;
    [SerializeField] private AudioClip exploseLarge;
    [SerializeField] private AudioClip exploseSmall;
    [SerializeField] private AudioClip castMeteor;
    [SerializeField] private AudioClip placeTower;

    // sound win - lose
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip lose;
    public bool check;
    private void Awake()
    {
        Instance = this;
        shortAudioSource = GetComponent<AudioSource>();
        longAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (check)
        {
            ImpactBulletSound();
            check = false;
        }
    }
    public void ImpactBulletSound()
    {
        shortAudioSource.PlayOneShot(impactBullet, 0.5f);
    }
    public void MeleePunchSound()
    {
        shortAudioSource.PlayOneShot(meleePunch, 1.0f);
    }
    public void MeleeDeahSound()
    {
        shortAudioSource.PlayOneShot(enemyMeleeDeath, 1.0f);
    }
    public void RangerDeathSound()
    {
        shortAudioSource.PlayOneShot(enemyRangerDeath, 1.0f);
    }
    public void DashSound()
    {
        shortAudioSource.PlayOneShot(dashSkill, 1.0f);
    }
    public void SmallExploseSound()
    {
        shortAudioSource.PlayOneShot(exploseSmall, 1.0f);
    }
    public void LargeExploseSound()
    {
        shortAudioSource.PlayOneShot(exploseLarge, 1.0f);
    }
    public void CastMeteorSound()
    {
        shortAudioSource.PlayOneShot(castMeteor, 1.0f);
    }
    public void PlaceTowerSound()
    {
        shortAudioSource.PlayOneShot(placeTower, 1.0f);
    }
    public void EndGameStopSound()
    {
        longAudioSource.Stop();
    }
}
