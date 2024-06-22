using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [field: SerializeField] public float HorizontalSpeed { get; set; } = 100f;
    [field: SerializeField] public float HorizontalAmplitude { get; set; } = 1f;
    [field: SerializeField] public float ForwardSpeed { get; set; }
    Vector3 _constraintStart;
    Vector3 _constraintEnd;

    public void Init(Vector3 constraintStart, Vector3 contrstaintEnd)
    {
        _constraintStart = constraintStart;
        _constraintEnd = contrstaintEnd;
    }

    void Update()
    {
        if (_constraintStart == null) return;

        transform.position += Time.deltaTime * ForwardSpeed * transform.forward;
        Vector3 moveVec = Mathf.Sin(Time.time * HorizontalSpeed) * HorizontalAmplitude * transform.right;
        transform.position += moveVec;
        float distanceToStart = Vector3.Dot(_constraintStart - transform.position, transform.right);
        float distanceToEnd = Vector3.Dot(transform.position - _constraintEnd, transform.right);

        if (distanceToStart < 0 || distanceToEnd < 0)
            transform.position -= moveVec;

        //Debug.DrawLine(transform.position, transform.position + transform.right * distanceToEnd, Color.red);
        //Debug.DrawLine(transform.position, _constraintEnd.position, Color.blue);
        //Debug.DrawLine(transform.position, transform.position + transform.right * distanceToStart, Color.red);
        //Debug.DrawLine(transform.position, _constraintStart.position, Color.blue);
    }

}
