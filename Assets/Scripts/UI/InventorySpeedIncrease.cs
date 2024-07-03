using System.Collections;
using UnityEngine;

public class InventorySpeedIncrease : MonoBehaviour
{
    [SerializeField] float _addition;
    [field: SerializeField] public float EffectDuration { get; private set; }

    public void Use(Player player)
    {
        foreach (Shooter shooter in player.Shooters)
        {
            shooter.BulletModifiers.Add(IncreaseSpeed);
            StartCoroutine(Timer(shooter));
        }
    }

    IEnumerator Timer(Shooter shooter)
    {
        yield return new WaitForSeconds(EffectDuration);

        shooter.BulletModifiers.Remove(IncreaseSpeed);

    }

    void IncreaseSpeed(Bullet blt)
    {
        blt.BulletSpeed += _addition;
    }
}
