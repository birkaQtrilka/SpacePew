using System;
using UnityEngine;
using UnityEngine.Events;

public class ColliderDamager : MonoBehaviour
{
    public event Action<GameObject, ColliderDamager> Damaged;
    [SerializeField] UnityEvent OnCollide;

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Health>(out var health)) return;

        health.TakeDamage(9999);

        Damaged?.Invoke(health.gameObject, this);
        OnCollide?.Invoke();
    }
}
