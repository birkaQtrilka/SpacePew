using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;

    protected abstract void OnPickUp(GameObject picker);

    void OnTriggerEnter(Collider other)
    {
        OnPickUp(other.gameObject);
        _particleSystem.Play();
        GetComponent<MeshRenderer>().enabled = false;
    }
}
