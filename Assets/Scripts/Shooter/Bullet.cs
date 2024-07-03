using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action<Bullet/*self*/, GameObject> Hit;
    [field: SerializeField] public int DamageAmount {  get; set; }
    [field: SerializeField] public float BulletSpeed { get; set; } = 1f;
    [field: SerializeField] public float BulletForce { get; set; } = 1f;
    [field: SerializeField] public float LifeTime { get; set; } = 3f;

    [SerializeField] ParticleSystem _particles;
    Rigidbody _ownerRigidBody;
    Collider _myCollider;
    Rigidbody _myRigidBody;

    void Awake()
    {
        _myCollider = GetComponent<Collider>();
        _myRigidBody = _myCollider.attachedRigidbody;
    }

    public void Init(Rigidbody shooterCol, LayerMask excludeLayers)
    {
        _ownerRigidBody = shooterCol;
        _myCollider.excludeLayers = excludeLayers;

    }

    void Update()
    {
        _myRigidBody.velocity = BulletSpeed * transform.forward;
        if ((LifeTime -= Time.deltaTime) <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (_ownerRigidBody == null || other.attachedRigidbody == _ownerRigidBody) return;
        
        if (!other.TryGetComponent<Health>(out var health)) return;

        health.TakeDamage(DamageAmount);
        _particles.Play();
        transform.GetChild(0).gameObject.SetActive(false);
        Hit?.Invoke(this, other.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Asteroid")) return;

        collision.rigidbody.AddForce((collision.transform.position - transform.position).normalized * BulletSpeed * BulletForce, ForceMode.Impulse);
        Destroy(gameObject);
        if (!collision.gameObject.TryGetComponent<Health>(out var health)) return;
        health.TakeDamage(DamageAmount);
        
    }

    void OnDestroy()
    {
        Hit = null;
    }
}
