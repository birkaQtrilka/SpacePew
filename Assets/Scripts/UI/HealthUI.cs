using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Health _targetHealth;
    [SerializeField] Image[] _fill;
    [SerializeField] GameObject _flashOverlay;
    [SerializeField] float _flashTime;
    Coroutine _currentFlash;

    void Awake()
    {
        _targetHealth.HealthChanged += UpdateUI;
    }


    void Start()
    {
        UpdateUI(_targetHealth);
    }
    
    void Flash()
    {
        if (_currentFlash != null)
            StopCoroutine(_currentFlash);

        _currentFlash = StartCoroutine(FlashCoroutine()); 
    }

    void UpdateUI(Health health)
    {
        float t = _targetHealth.Value / (float)_targetHealth.Max;

        int index = Mathf.FloorToInt((_fill.Length - 1) * t);
        //Debug.Log($"index: {(_fill.Length - 1) * t}, health: {_targetHealth.Value}");
        for (int i = 0; i < _fill.Length; i++)
        {
            _fill[i].gameObject.SetActive(i <= index);
        }
        if (_targetHealth.Value == 0)
            _fill[0].gameObject.SetActive(false);

        Flash();
    }

    IEnumerator FlashCoroutine()
    {
        _flashOverlay.SetActive(true);
        yield return new WaitForSeconds(_flashTime);
        _flashOverlay.SetActive(false);


    }

    void OnDestroy()
    {
        _targetHealth.HealthChanged -= UpdateUI;

    }
}
