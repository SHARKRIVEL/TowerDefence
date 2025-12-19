using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class MachineGun : Weapon
{
    bool triggered = false;

    void OnEnable()
    {
        triggered = false;
    }

    protected override void Firing()
    {
        if(enemyLook)
        {
            float targetPos = Vector3.Distance(transform.position,enemyLook.transform.position);
            if(targetPos<weaponRange && !scoreBoard.gamePos && enemyLook.gameObject.activeInHierarchy)
            {
                enemyDitected = true;
                if(!triggered)
                {
                    StartCoroutine(fireRateBasedAudio());
                }
                transform.LookAt(enemyLook.transform.position);
            }
            else
            {
                StopCoroutine(fireRateBasedAudio());
                enemyDitected = false; 
                triggered = false;
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
}
