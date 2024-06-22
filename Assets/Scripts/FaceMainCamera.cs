using UnityEngine;

public class FaceMainCamera : MonoBehaviour
{
    Camera _mainCam;

    void Awake()
    {
        _mainCam = Camera.main;
        GetComponent<Canvas>().worldCamera = _mainCam;
    }

    void Update()
    {
        transform.LookAt(_mainCam.transform);
    }
}
