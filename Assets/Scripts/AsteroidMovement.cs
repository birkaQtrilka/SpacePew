using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class AsteroidMovement : MonoBehaviour
{
    [field: SerializeField] public float ForcePower { get; set; } = 1;
    [SerializeField] bool xLocalConstraint;
    [SerializeField] bool yLocalConstraint;
    [SerializeField] bool zLocalConstraint;
    Rigidbody _rb;
    
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.drag = 0;
        _rb.AddForce(transform.forward * ForcePower, ForceMode.Impulse);

        transform.GetChild(0).rotation = Random.rotation;
    }

    void Update()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(_rb.velocity);
        if (xLocalConstraint)
            localVelocity.x = 0;
        if (yLocalConstraint)
            localVelocity.y = 0;
        if (zLocalConstraint)
            localVelocity.z = 0;

        _rb.velocity = transform.TransformDirection(localVelocity);
    }
}
