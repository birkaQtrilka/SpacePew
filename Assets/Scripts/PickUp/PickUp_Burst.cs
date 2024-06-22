using System;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Burst : PickUp
{
    [SerializeField] BurstData _burstData;

    protected override void OnPickUp(GameObject picker)
    {
        Player player = picker.transform.parent.GetComponent<Player>();
        Shooter[] shooters = player.Shooters;

        foreach (Shooter shooter in shooters)
        {
            if(shooter is not ShooterBurst)
            {
                GameObject holder = shooter.gameObject;
                var burst = holder.AddComponent<ShooterBurst>();
                shooter.ChangeToShooter(burst);
                Destroy(shooter);
                player.ShootCooldown += _burstData.ÌnbetweenCooldown * _burstData.BurstCount;
                burst.BurstInit(_burstData);

                List<Action<Bullet>> bltMods = new(shooter.BulletModifiers);
                burst.Init(shooter.MyRigidBody, shooter.ExcludeLayers, bltMods);
            }
        }
    }
}
