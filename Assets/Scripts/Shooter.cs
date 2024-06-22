using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public abstract class Shooter : MonoBehaviour
{
    public event Action<Shooter, Shooter/*oldShooter, newShooter*/> ShooterChange;
    public event Action<Bullet, GameObject/*bullet that hit, hit GameObject*/> BulletHit;

    public List<Action<Bullet>> BulletModifiers { get; private set; } = new();
    
    [field: SerializeField] public Rigidbody MyRigidBody { get; private set; }//make enemy bullet
    [field: SerializeField] public LayerMask ExcludeLayers { get; private set; }
    [SerializeField] float _bulletStartSpeed = 60;
    [SerializeField] float _bulletStartLifeTime = 3;
    protected ParticleSystem shootParticles;

    protected void Awake()
    {
        shootParticles = GetComponent<ParticleSystem>();
    }

    public void Init(Rigidbody myCol, LayerMask excludeLayers, List<Action<Bullet>> bulletModifiers)
    {
        MyRigidBody = myCol;
        ExcludeLayers = excludeLayers;
        BulletModifiers = bulletModifiers;
    }

    public void Shoot()
    {
        List<Bullet> spawnedBullets = ShootBullet();

        foreach (var bullet in spawnedBullets)
        {
            bullet.BulletSpeed = _bulletStartSpeed;
            bullet.LifeTime = _bulletStartLifeTime;
            foreach (var bulletModifier in BulletModifiers)
                bulletModifier.Invoke(bullet);
            bullet.Hit += OnBulletHit;
            bullet.Init(MyRigidBody, ExcludeLayers);
        }
        
    }

    void OnBulletHit(Bullet bullet, GameObject obj)
    {
        BulletHit?.Invoke(bullet, obj);
        BulletHit -= OnBulletHit;
    }

    public void ChangeToShooter(Shooter newShooter)
    {
        ShooterChange?.Invoke(this, newShooter);
    }

    protected abstract List<Bullet> ShootBullet();
    
    private void OnDestroy()
    {
        ShooterChange = null;    
    }
}
