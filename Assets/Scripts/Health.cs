using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
	public event Action<Health> HealthChanged;
	public int Max => _maxHealth;
	public int Value => _health;

	[SerializeField] int _maxHealth = 10;
	[SerializeField] int _health = 10;
	[SerializeField] GameObject _objectToDestroy;
	[SerializeField] UnityEvent _onHealthZero;
	[SerializeField] UnityEvent _onHealthChanged;

    private void Start()
    {
		if (_objectToDestroy == null)
			_objectToDestroy = gameObject;
    }
    public void TakeDamage(int damage) {
		if (_health == 0) return;
        _health -= damage;

        if (_health<=0) {
			_health = 0;
			_onHealthZero.Invoke();
		}
		else
			_onHealthChanged.Invoke();
        HealthChanged?.Invoke(this);

	}

	public void DestroyObject()
	{
		Destroy(_objectToDestroy);
	}

    private void OnDestroy()
    {
		HealthChanged = null;
    }
}
