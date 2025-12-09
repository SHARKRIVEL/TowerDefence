using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class Weapon : MonoBehaviour
{
    [SerializeField] ParticleSystem[] bulletFiringComponents;

    [SerializeField] float weaponRange = 30f; 

    [SerializeField] WeaponType weaponType;

    [SerializeField] AudioSource audioSource;

    bool triggered = false;
    bool enemyDitected = false;
    float distanceBtwenemyAndWeaponRange;
    public int bulletFireRate = 2;
    enemy enemyLook;

    ScoreBoard scoreBoard;

    void Start()
    {
        foreach(ParticleSystem bulletRateOfFireInc in bulletFiringComponents)
        {
            var emissionsRateOfFire = bulletRateOfFireInc.emission;
            emissionsRateOfFire.enabled = false;
        }
        scoreBoard = FindFirstObjectByType<ScoreBoard>();
    }

    void Update()
    {
        WeaponFacing();
        Firing();
    }

    void WeaponFacing()
    {
        enemy[] enemies = FindObjectsOfType<enemy>();
        float maxDist = Mathf.Infinity;
        foreach(enemy enemy in enemies)
        {
            distanceBtwenemyAndWeaponRange = Vector3.Distance(transform.position,enemy.transform.position);
            if(distanceBtwenemyAndWeaponRange<maxDist)
            {    
                enemyLook = enemy;
                maxDist = distanceBtwenemyAndWeaponRange;
            }
        }
    }

    void Firing()
    {
        if(enemyLook)
        {
            float targetPos = Vector3.Distance(transform.position,enemyLook.transform.position);
            if(targetPos<weaponRange && !scoreBoard.gamePos && enemyLook.gameObject.activeInHierarchy)
            {

                enemyDitected = true;
                if(weaponType.index==1)
                {
                    if(!triggered)
                    {
                        StartCoroutine(fireRateBasedAudio());
                    }
                }
                else{
                    audioSource.Play();
                }
                transform.LookAt(enemyLook.transform.position);
            }
            else
            {
                StopCoroutine(fireRateBasedAudio());
                enemyDitected = false; 
                triggered = false;
                audioSource.Stop();
            }
            foreach(ParticleSystem bulletRateOfFireInc in bulletFiringComponents)
            {
                var emissionsRateOfFire = bulletRateOfFireInc.emission;
                emissionsRateOfFire.enabled = enemyDitected;
            }   
        }
    }

    IEnumerator fireRateBasedAudio()
    {
         triggered= true;
        while(enemyDitected)
        {
            audioSource.Play();
            yield return new WaitForSeconds(bulletFireRate);
        }
    }

    public void BulletDamager(int bulletDamage)
    {
        this.bulletFireRate += bulletDamage;
        foreach(ParticleSystem bulletRateOfFireInc in bulletFiringComponents)
        {
            var emissionsRateOfFire = bulletRateOfFireInc.emission;
            emissionsRateOfFire.rateOverTime = bulletFireRate;
        }
    }
}
