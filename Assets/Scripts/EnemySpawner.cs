using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static bool CanSpawn { get; set; } = true;

    [SerializeField] GameObject[] _enemyVariants;

    [field: SerializeField] public float SpawnTimerMin;
    [field: SerializeField] public float SpawnTimerMax;
    [SerializeField] Transform _spawnStart;
    [SerializeField] Transform _spawnEnd;

    float _currTimer;

    void Update()
    {
        if (!CanSpawn) return;
        _currTimer -= Time.deltaTime;
        if (_currTimer <= 0)
        {
            Vector3 spawnPos = Vector3.Lerp(_spawnStart.position, _spawnEnd.position, Random.Range(0f, 1f));
            GameObject enemyObj = Instantiate(_enemyVariants[Random.Range(0, _enemyVariants.Length)],spawnPos, transform.rotation);
            enemyObj.GetComponent<EnemyMovement>()?.Init(_spawnStart.position, _spawnEnd.position);
            _currTimer = Random.Range(SpawnTimerMin, SpawnTimerMax);
        }
    }
}
