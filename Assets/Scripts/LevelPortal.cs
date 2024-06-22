using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] Image _screenDimmer;
    [SerializeField] float _dimmingTime;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            StartCoroutine(DoDimming());

    }

    IEnumerator DoDimming()
    {
        _screenDimmer.gameObject.SetActive(true);

        float currTime = 0;
        Color currColor = _screenDimmer.color;

        while (currTime < _dimmingTime)
        {
            _screenDimmer.color = new Color(currColor.r, currColor.g, currColor.b, currTime / _dimmingTime);
            currTime += Time.deltaTime;
            yield return null;
            
        }
        _screenDimmer.color = new Color(currColor.r, currColor.g, currColor.b, 1);
        yield return null;
        SceneHandler.Instance.NextLevel();
    }
}
