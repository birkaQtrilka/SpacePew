using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action<int> ScoreUpdated;
    public static ScoreManager Instance { get; private set; }
    [field: SerializeField] public int Score { get; private set; }
    public int HighScore => _scoreData.BestScore;
    public int LocalHighScore { get; private set; }

    [SerializeField] ScoreData _scoreData;

    void Awake()
    {
        if(Instance !=  null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        SceneHandler.Instance.SceneLoaded += OnScceneLoad;

    }

    void OnScceneLoad()
    {
        Score = 0;
        IncreaseScore(0);
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
        ScoreUpdated?.Invoke(Score);

        if(Score > _scoreData.BestScore)
            _scoreData.BestScore = Score;
        if(Score >  LocalHighScore)
            LocalHighScore = Score;
    }

    void OnDestroy()
    {
        if(SceneHandler.Instance != null)
            SceneHandler.Instance.SceneLoaded -= OnScceneLoad;

    }
}
