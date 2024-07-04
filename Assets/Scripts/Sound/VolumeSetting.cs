using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] Slider _slider;
    
    void Start()
    {
        UpdateVolumeData();
        _slider.onValueChanged.AddListener(SoundManager.Instance.ChangeMasterVolume);
    }

    void UpdateVolumeData()
    {
        _slider.value = SoundManager.Instance.MasterVolume;
    }

}
