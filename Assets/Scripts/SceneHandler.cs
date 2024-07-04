using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }
    public event Action SceneLoaded;
    Scene _currentScene;

    [SerializeField] bool _next;
    [SerializeField] int _load = -1;
    [SerializeField] bool _restart;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
        
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void Update()
    {
        if (_load > -1)
        {
            LoadLevel(_load);
            _load = -1;
        }    else if(_restart)
        {
            RestartScene();
            _restart = false;
        } else if (_next)
        {
            NextLevel();
            _next = false;
        }
    }

    void OnSceneLoad(Scene currScene, LoadSceneMode arg1)
    {
        _currentScene = currScene;

        SceneLoaded?.Invoke();
    }

    public void NextLevel()
    {
        int nextIndex = _currentScene.buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextIndex) 
            SceneManager.LoadScene(nextIndex);
        else 
            SceneManager.LoadScene(0);
    }
    
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);

    }

    public void RestartScene()
    {
        SceneManager.LoadScene(_currentScene.buildIndex);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        SceneLoaded = null;

    }

    void OnDestroy()
    {
        SceneLoaded = null;
    }
}
