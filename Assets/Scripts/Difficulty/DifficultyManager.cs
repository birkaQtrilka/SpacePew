using System;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public event Action<float> DifficultyIncreased;
    public static DifficultyManager Instance { get; private set; }
    public float CurrentDifficulty { get; private set; }
    [field:SerializeField] public ModifyByDifficultyData Data { get; private set; }


    [SerializeField] int _maxScoreToIncreaseDifficulty;
    [SerializeField] AnimationCurve _difficultyIncreaseCurve;
    ScoreManager _scoreManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
            Instance = this;

    }
    void Start()
    {
        _scoreManager = ScoreManager.Instance;
        _scoreManager.ScoreUpdated += OnScoreUpdated;

    }

    void OnScoreUpdated(int newScore)
    {
        float difficultyValue = _difficultyIncreaseCurve.Evaluate(newScore / (float)_maxScoreToIncreaseDifficulty);
        
        CurrentDifficulty = Mathf.Clamp01(difficultyValue);

        DifficultyIncreased?.Invoke(CurrentDifficulty);
    }

    void OnDestroy()
    {
        DifficultyIncreased = null;    
        Instance = null;
    }
}
