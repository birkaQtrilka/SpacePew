using UnityEngine;

public class SoundAdapter : MonoBehaviour
{
    [SerializeField] SoundName _soundName;
    [SerializeField] string _name;
    [SerializeField, Range(0,1)] float _volume = 1;
    [SerializeField] bool _loop = false;

    public void PlaySound()
    {
        SoundManager.Instance.PlaySound(_soundName,_volume,_name, _loop);
    }

    public void StopSound()
    {
        if(string.IsNullOrEmpty(_name))
        {
            return;
        }
        SoundManager.Instance.StopSound(_name);
    }
}
