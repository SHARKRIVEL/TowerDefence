using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class Weapon : MonoBehaviour
{
    public ParticleSystem[] bulletFiringComponents;
    public float weaponRange;
    public WeaponType weaponType;
    public AudioSource audioSource;
    public bool enemyDitected = false;
    float distanceBtwenemyAndWeaponRange;
    float bulletsPerSecond;
    public float bulletFireRate;
    public enemy nearestEnemy;
    public ScoreBoard scoreBoard;
    TowersPool towersPool;


    protected virtual void Start()
    {
        bulletsPerSecond = weaponType.FireRate;
        bulletFireRate = 1f/bulletsPerSecond;

        towersPool = GetComponentInParent<TowersPool>();
        foreach(ParticleSystem bulletRateOfFireInc in bulletFiringComponents)
        {
            var emissionsRateOfFire = bulletRateOfFireInc.emission;
            emissionsRateOfFire.enabled = false;
        }
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
    }

    protected virtual void Update()
    {
        WeaponFacing();
        Firing();
    }

    void WeaponFacing()
    {
        if(towersPool.enemies.Count <= 0) return;
        
        float maxDist = Mathf.Infinity;
        foreach(enemy enemy in towersPool.enemies)
        {
            distanceBtwenemyAndWeaponRange = Vector3.Distance(transform.position,enemy.transform.position);
            if(distanceBtwenemyAndWeaponRange<maxDist)
            {    
                nearestEnemy = enemy;
                maxDist = distanceBtwenemyAndWeaponRange;
            }
        }
    }

    protected virtual void Firing()
    {
        if(nearestEnemy)
        {
            float targetPos = Vector3.Distance(transform.position,nearestEnemy.transform.position);
            if(targetPos<weaponRange && !scoreBoard.gamePos && nearestEnemy.gameObject.activeInHierarchy)
            {
                enemyDitected = true;
                if(!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                transform.LookAt(nearestEnemy.transform.position);
            }
            else
            {
                enemyDitected = false; 
                audioSource.Stop();
            }
            foreach(ParticleSystem bulletRateOfFireInc in bulletFiringComponents)
            {
                var emissionsRateOfFire = bulletRateOfFireInc.emission;
                emissionsRateOfFire.enabled = enemyDitected;
            }   
        }
    }

    public virtual void BulletDamager(float fireRate)
    {
        bulletsPerSecond += fireRate;
        this.bulletFireRate = 1f/bulletsPerSecond;
        foreach(ParticleSystem bulletRateOfFireInc in bulletFiringComponents)
        {
            var emissionsRateOfFire = bulletRateOfFireInc.emission;
            emissionsRateOfFire.rateOverTime = bulletsPerSecond;
        }
    }
}
