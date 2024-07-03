using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] Health _health;

    void Awake()
    {
        _health.HealthChanged += OnHealthUpdate;
    }

    void OnHealthUpdate(Health health)
    {
        _fillImage.fillAmount = health.Value / (float)health.Max;
    }
}
