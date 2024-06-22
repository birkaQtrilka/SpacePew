using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [field: SerializeField] public float HorizontalSpeed { get; set; } = 100f;
    [field: SerializeField] public float ForwardSpeed { get; set; } = 50f;
    public bool CanMoveForward { get; set; } = true;

    [SerializeField] Transform _playerModel;
    [SerializeField] float _constraints = 2;
    [SerializeField] bool _reset;
    [SerializeField] float _maxTilt;

    Vector3 _startPos;

    void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        Vector3 dir = new(Input.GetAxisRaw("Horizontal"), 0, 0);

        Vector3 moveVector = HorizontalSpeed * Time.deltaTime * dir.normalized;
        _playerModel.localPosition += moveVector;
        
        float side = Vector3.Dot(_playerModel.position - transform.position, transform.right);
        float tiltPersentage = side / _constraints;
        _playerModel.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, tiltPersentage * _maxTilt);

        ConstraintMovement(moveVector);
        if(CanMoveForward)
            transform.position += Time.deltaTime * ForwardSpeed * transform.forward;


        if(_reset)
        {
            transform.position = _startPos;
            _reset = false;
        }

    }

    void ConstraintMovement(Vector3 vel)
    {
        if ((_playerModel.position - transform.position).magnitude >= _constraints)
            _playerModel.localPosition -= vel;
    }
}
