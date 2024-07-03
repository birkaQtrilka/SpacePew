using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [field: SerializeField] public bool CanShoot { get; set; } = true;

    [SerializeField] float _cooldown;
    [SerializeField] Shooter[] _shooters;
    float _currCooldown;

    void Update()
    {
        if(!CanShoot) return;

        if(_currCooldown >= _cooldown)
        {
            foreach(Shooter shooter in _shooters)
            {
                shooter.Shoot();
            }
            _currCooldown = 0;
        }
        _currCooldown += Time.deltaTime;
    }
}
