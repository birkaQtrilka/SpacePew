using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _followSpeed;
    [SerializeField] Vector3 _offset;
    private void Start()
    {
        _offset = _target.InverseTransformPoint(transform.position);
        //Console.WriteLine(_offset + $", old way: {_target.position - transform.position}");
    }

    void LateUpdate()
    {
        if (_target == null) return;

        //transform.eulerAngles = new Vector3(_target.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, _target.TransformPoint(_offset), _followSpeed * Time.deltaTime), _target.rotation);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, _target.eulerAngles.y);
    }
}
