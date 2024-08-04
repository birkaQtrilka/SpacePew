using UnityEngine;
[RequireComponent (typeof(EnemyMovement))]
public class MoveSpeedModifier : MonoBehaviour
{
    DifficultyManager _difficultyManager;
    EnemyMovement _enemyMovement;

    void Start()
    {
        _difficultyManager = DifficultyManager.Instance;
        _difficultyManager.DifficultyIncreased += OnDifficultyIncrease;

        _enemyMovement = GetComponent<EnemyMovement>();
    }

    void OnDifficultyIncrease(float currDifficulty)
    {
        _enemyMovement.ForwardSpeed = _enemyMovement.BaseForwardSpeed * _difficultyManager.Data.DifficultyToMoveSpeed.Evaluate(currDifficulty);
    }

    void OnDestroy()
    {
        if (_difficultyManager != null)
            _difficultyManager.DifficultyIncreased -= OnDifficultyIncrease;

    }
}
