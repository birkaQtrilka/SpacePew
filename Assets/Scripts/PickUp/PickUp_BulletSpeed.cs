using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_BulletSpeed : PickUp
{
    [SerializeField] float _speedIncrease;
    protected override void OnPickUp(GameObject picker)
    {
        Shooter[] shooters = picker.GetComponentsInChildren<Shooter>();
        foreach (Shooter shooter in shooters)
        {
            shooter.BulletModifiers.Add(b => b.BulletSpeed += _speedIncrease);
        }
    }
}
