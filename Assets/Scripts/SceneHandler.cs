using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }
    public int CurrLevel { get; set; } = 0;
    public event Action SceneLoaded;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
        
        CurrLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        SceneLoaded?.Invoke();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(++CurrLevel);
    }
    
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);

    }

    public void RestartScene()
    {
        SceneManager.LoadScene(CurrLevel);
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
