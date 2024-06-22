using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_ShootCooldown : PickUp
{
    [SerializeField] float _addition;

    protected override void OnPickUp(GameObject picker)
    {
        picker.GetComponentInParent<Player>().ShootCooldown += _addition;
    }
}
