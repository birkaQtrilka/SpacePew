using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public float ShootCooldown { get; set; }
    [field: SerializeField] public Shooter[] Shooters { get; private set; }
    
    [SerializeField] Shaker _cameraShaker;

    Barrier _barrier;
    Health _health;
    float _currCooldown;

    void Awake()
    {
        _health = GetComponentInChildren<Health>();
        _barrier = GetComponentInChildren<Barrier>();

        _barrier.ItemDestroy += OnBarierHit;
        foreach (var shooter in Shooters)
        {
            ConnectShooter(shooter);
        }
    }

    void ConnectShooter(Shooter shooter)
    {
        shooter.ShooterChange += OnShooterChange;
        shooter.BulletHit += CheckEnemyKill;
    }

    void DisconnectShooter(Shooter shooter)
    {
        shooter.ShooterChange -= OnShooterChange;
        shooter.BulletHit -= CheckEnemyKill;
    }

    void OnShooterChange(Shooter oldShooter, Shooter newShooter)
    {
        int oldShooterIndex = Array.IndexOf(Shooters, oldShooter);
        if (oldShooterIndex == -1) return;

        Shooters[oldShooterIndex] = newShooter;

        DisconnectShooter(oldShooter);
        ConnectShooter(newShooter);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _currCooldown > ShootCooldown)
        {
            _currCooldown = 0;
            foreach(var shooter in Shooters)
            {
                shooter.Shoot();
                SoundManager.Instance.PlaySound(SoundName.Shoot);
            }
            _cameraShaker.Shake();
        }
        _currCooldown += Time.deltaTime;

    }

    public void OnBarierHit(GameObject destroyedItem)
    {
        if (!destroyedItem.CompareTag("Enemy")) return;
        
        _health.TakeDamage(1);
    }

    void CheckEnemyKill(Bullet blt, GameObject hitObj)
    {
        if(!hitObj.TryGetComponent<Health>(out var enemyHealth)) return;

        if(enemyHealth.Value == 0 && enemyHealth.TryGetComponent<ScoreStat>(out var scoreStat))
        {
            ScoreManager.Instance.IncreaseScore(scoreStat.Value);
        }
    }

    void OnDestroy()
    {
        _barrier.ItemDestroy -= OnBarierHit;

        foreach (var shooter in Shooters)
        {
            DisconnectShooter(shooter);
        }
    }
}
