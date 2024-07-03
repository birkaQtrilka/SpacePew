using System;
using System.Collections.Generic;
using UnityEngine;

public class ShooterModifierByDifficulty : MonoBehaviour
{
    [SerializeField] ModifyByDifficultyData _data;
    [SerializeField] Shooter[] _shooters;

    readonly List<Action<Bullet>> _bulletMods = new();
    DifficultyManager _difficultyManager;

    void Start()
    {
        _difficultyManager = DifficultyManager.Instance;
        _difficultyManager.DifficultyIncreased += OnDifficultyIncrease;
        OnDifficultyIncrease(_difficultyManager.CurrentDifficulty);
    }

    void OnDifficultyIncrease(float currDifficulty)
    {
        foreach (var bltMod in _bulletMods)
        {
            foreach (var shooter in _shooters)
                shooter.BulletModifiers.Remove(bltMod);
        }
        _bulletMods.Clear();

        _bulletMods.Add(bullet =>
        {
            bullet.DamageAmount = (int)((_data.DifficultyToDamage.Evaluate(currDifficulty) + 1) * bullet.DamageAmount);
        });
        _bulletMods.Add(bullet => 
        {
            bullet.BulletSpeed *= 1 + _data.DifficultyToSpeed.Evaluate(currDifficulty); 
        }
        );
        _bulletMods.Add(bullet =>
        {
            Material mat = _data.MaterialThreshHold.TryGetMaterial(currDifficulty);
            if (mat != null)
                bullet.gameObject.GetComponentInChildren<MeshRenderer>().material = mat;
        }
        );

        foreach (var bltMod in _bulletMods)
        {
            foreach (var shooter in _shooters)
                shooter.BulletModifiers.Add(bltMod);
        }
    }

}
