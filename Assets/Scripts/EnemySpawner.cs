using System;
using UnityEngine;
using Random = UnityEngine.Random;
[Serializable]
public class SpawnItem
{
    public GameObject ToSpawn;
    public float Chance;
}
public class EnemySpawner : MonoBehaviour
{
    public static bool CanSpawn { get; set; } = true;

    [SerializeField] SpawnItem[] _enemyVariants;

    [field: SerializeField] public float SpawnTimerMin { get; set; }
    [field: SerializeField] public float SpawnTimerMax { get; set; }

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
            
            GameObject enemyObj = SpawnRandomEnemy(spawnPos);

            if (enemyObj.TryGetComponent<EnemyMovement>(out var enemyMovement))
                enemyMovement.Init(_spawnStart.position, _spawnEnd.position);

            _currTimer = Random.Range(SpawnTimerMin, SpawnTimerMax);
        }
    }

    GameObject SpawnRandomEnemy(Vector3 spawnPos)
    {
        float totalChance = 0;
        foreach (var enemy in _enemyVariants)
        {
            totalChance += enemy.Chance;
        }

        float rand = Random.Range(0, totalChance);
        float cumulativeChance = 0;

        foreach (var item in _enemyVariants)
        {
            cumulativeChance += item.Chance;

            if(rand <= cumulativeChance)
                return Instantiate(item.ToSpawn, spawnPos, transform.rotation);
        }

        Debug.LogError("SpawnRandomEnemy returns null, should be impossible");
        return null;
    }
}
