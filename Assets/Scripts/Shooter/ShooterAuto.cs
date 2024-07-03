using System.Collections.Generic;
using UnityEngine;

public class ShooterAuto : Shooter
{
    [SerializeField] Bullet _bulletPrefab;

    protected override List<Bullet> ShootBullet()
    {
        shootParticles.Play();
        return new List<Bullet>(1)
        {
            Instantiate(_bulletPrefab, transform.position, transform.rotation)
        };
        
    }
}
