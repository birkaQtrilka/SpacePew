using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    [SerializeField] bool _canWobble;
    [SerializeField] float _wobbleFrequency;
    [SerializeField] float _wobbleAmplitude;
    [SerializeField] Transform _pivot;
    private void Update()
    {
        if(_canWobble)
        {
            transform.position = _pivot.position + _wobbleAmplitude * Mathf.Sin(Time.time * _wobbleFrequency) * Vector3.up;
        }
    }
}
