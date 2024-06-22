using System.Collections;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] float _intensity;
    [SerializeField] float _duration;

    public void Shake()
    {
        StartCoroutine(ShakeLoop());
    }

    IEnumerator ShakeLoop()
    {
        float currDir = 0;
        Vector3 initialPos = Vector3.zero;
        while (currDir < _duration)
        {
            Vector3 randomDir = Random.insideUnitSphere * _intensity;
            transform.localPosition = initialPos + randomDir;
            yield return null;
            currDir += Time.deltaTime;
        }
        transform.localPosition = initialPos;
    }
}
