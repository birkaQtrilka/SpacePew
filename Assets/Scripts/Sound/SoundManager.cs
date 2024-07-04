using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct SoundData
{
    public SoundName Name;
    [Range(0f, 1f)]
    public float Volume;
    public AudioClip Clip;
    
}

public enum SoundName
{
    Button,
    Shoot,
    AsteroidHit,
    Explosion,
    ShipHit
}

[Serializable]
class SourceInfo
{
    public AudioSource AudioSource;
    public string Name;
    
    public SourceInfo(AudioSource audioSource, string name)
    {
        AudioSource = audioSource;
        Name = name;
    }
}

public class SoundManager : MonoBehaviour
{
    readonly List<SourceInfo> _activeSources = new();
    Stack<AudioSource> _inactiveSources;
    public static SoundManager Instance { get; private set; }
    public List<SoundData> SoundClips;
    [field: SerializeField, Range(0, 1)] public float MasterVolume { get; private set; }

    void Awake()
    {
        //Debug.Log("song awake");
        if (Instance != null && Instance != this)
        {
            Debug.Log("destroying song manager");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _inactiveSources = new Stack<AudioSource>(GetComponents<AudioSource>());
        foreach (AudioSource source in _inactiveSources)
            InitSource(source);
        DontDestroyOnLoad(this);
    }



    public bool ContainsSoundWithName(string name)
    {
        return _activeSources.Any(s => s.Name == name);
    }

    bool TryGetSound(SoundName soundName, out SoundData sound)
    {
        sound = SoundClips.FirstOrDefault(s => s.Name == soundName);
        return sound.Clip != null;
    }

    public void ChangeMasterVolume(float volume)
    {
        MasterVolume = volume;
    }

    public void PlaySound(SoundName soundName)
    {
        if (!TryGetSound(soundName, out SoundData soundData))
        {
            Debug.LogError($"There is no sound named {soundName}");
            return;
        }
        var source = GetSource();
        source.clip = soundData.Clip;
        source.volume = soundData.Volume * MasterVolume;
        source.Play();
    }

    public void PlaySound(SoundName soundName, float volumeMult = 1, string name = null, bool loop = false)
    {
        if (!TryGetSound(soundName, out SoundData data))
        {
            Debug.LogError($"There is no sound named {soundName}");
            return;
        }

        AudioSource source = GetSource(name);
        source.clip = data.Clip;
        source.volume = data.Volume * volumeMult * MasterVolume;
        source.loop = loop;
        source.Play();
    }

    public void StopSound(string name)
    {
        SourceInfo source = FindSourceInfoByName(name);
        if(source != null)
        {
            source.AudioSource.Stop();
            ReleaseSource(source);
        }
    }


    void InitSource(AudioSource source)
    {
        source.playOnAwake = false;
    }

    AudioSource GetSource()
    {
        AudioSource source;
        if (_inactiveSources.Count == 0)
        {
            source = gameObject.AddComponent<AudioSource>();
            InitSource(source);
        }
        else
            source = _inactiveSources.Pop();

        _activeSources.Add(new SourceInfo(source,null));
        source.enabled = true;
        source.loop = false;
        source.volume = MasterVolume; 
        return source;

    }

    AudioSource GetSource(string nameSource)
    {
        AudioSource source;
        if (_inactiveSources.Count == 0)
        {
            source = gameObject.AddComponent<AudioSource>();
            InitSource(source);
        }
        else
            source = _inactiveSources.Pop();

        _activeSources.Add(new SourceInfo(source, nameSource));

        source.loop = false;
        source.volume = MasterVolume; 
        source.enabled = true;

        return source;

    }

    SourceInfo FindSourceInfoByName(string name)
    {
        return _activeSources.FirstOrDefault(s => s.Name == name);
    }

    void ReleaseSource(SourceInfo audioSource)
    {
        _activeSources.Remove(audioSource);
        audioSource.AudioSource.enabled = false;
        _inactiveSources.Push(audioSource.AudioSource);
    }

    void Update()
    {
        for (int i = 0; i < _activeSources.Count; i++)
        {
            var source = _activeSources[i];
            if (source.AudioSource.time >= source.AudioSource.clip.length || (source.AudioSource.time == 0 && !source.AudioSource.isPlaying))
            {
                ReleaseSource(source);
                i--;
            }
        }
    }
}
