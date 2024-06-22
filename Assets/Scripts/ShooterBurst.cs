using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[Serializable]
public struct BurstData
{
    public Bullet BulletPrefab;
    public int BurstCount;
    public float ÌnbetweenCooldown;
}

public class ShooterBurst : Shooter
{
    [SerializeField] BurstData _burstData;
    public void BurstInit(BurstData burstData)
    {
        _burstData = burstData;
    }
    
    protected override List<Bullet> ShootBullet()
    {
        List<Bullet> soonToBeShotBullets = new(_burstData.BurstCount);
        for (int i = 0; i < _burstData.BurstCount; i++)
        {
            Bullet newBullet = Instantiate(_burstData.BulletPrefab);
            newBullet.gameObject.SetActive(false);
            soonToBeShotBullets.Add(newBullet);
        }
        StartCoroutine(DoBurst(soonToBeShotBullets));   
        
        return soonToBeShotBullets;
    }

    IEnumerator DoBurst(List<Bullet> bullets)
    {
        WaitForSeconds wait = new(_burstData.ÌnbetweenCooldown); 
        for (int i = 0; i < _burstData.BurstCount; i++)
        {
            yield return wait;
            shootParticles.Play();
            bullets[i].gameObject.SetActive(true);
            bullets[i].transform.SetPositionAndRotation(transform.position, transform.rotation);

        }
    }
}
