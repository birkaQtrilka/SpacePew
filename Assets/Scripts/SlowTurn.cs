using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTurn : MonoBehaviour
{
    [SerializeField] Vector3 _turnSpeed;

    void FixedUpdate()
    {
        transform.Rotate(_turnSpeed);
    }
}
