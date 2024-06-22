using UnityEngine;

public class PickUp_PlayerSpeed : PickUp
{
    [SerializeField] float _addSpeed;

    protected override void OnPickUp(GameObject picker)
    {
        picker.GetComponentInParent<PlayerControls>().ForwardSpeed += _addSpeed;
    }
}
