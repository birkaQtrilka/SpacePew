using UnityEngine;

[RequireComponent (typeof(EnemySpawner))]
public class SpawnByDifficultyManager : MonoBehaviour
{
    DifficultyManager _difficultyManager;
    EnemySpawner _enemySpawner;

    float _startMin;
    float _startMax;

    void Start()
    {
        _difficultyManager = DifficultyManager.Instance;
        _difficultyManager.DifficultyIncreased += OnDifficultyIncrease;

        _enemySpawner = GetComponent<EnemySpawner>();
        _startMin = _enemySpawner.SpawnTimerMin;
        _startMax = _enemySpawner.SpawnTimerMax;
    }

    void OnDifficultyIncrease(float currDifficulty)
    {
        _enemySpawner.SpawnTimerMin = _startMin - _difficultyManager.Data.DifficultyToSpawnRate.Evaluate(currDifficulty);
        _enemySpawner.SpawnTimerMax = _startMax - _difficultyManager.Data.DifficultyToSpawnRate.Evaluate(currDifficulty);
    }

    void OnDestroy()
    {
        if( _difficultyManager != null ) 
            _difficultyManager.DifficultyIncreased -= OnDifficultyIncrease;

    }
}
