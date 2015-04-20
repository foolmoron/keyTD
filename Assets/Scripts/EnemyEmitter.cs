using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyEmitter : MonoBehaviour {

    public Enemy EnemyPrefab;
    public Vector2 EnemySpawnVariance;
    public Transform EnemyTarget;
    public Vector2 EnemyTargetVariance;
    [Range(0, 10)]
    public float HealthModifier = 1;
    [Range(0, 10)]
    public float MoneyModifier = 1;

    public bool SpawningEnemies;
    [Range(0, 60)]
    public float EnemySpawnInterval;
    [Range(0, 30)]
    public float TimeToNextSpawn;

    [HideInInspector]
    public List<Enemy> liveEnemiesSpawned = new List<Enemy>(50);
    WaveTimer waveTimer;

    void Start() {
        waveTimer = FindObjectOfType<WaveTimer>();
    }

    void Update() {
        if (SpawningEnemies) {
            TimeToNextSpawn -= Time.deltaTime;
            if (TimeToNextSpawn <= 0) {
                Spawn();
            }
        }

        if (liveEnemiesSpawned.Count > 0) {
            for (int i = 0; i < liveEnemiesSpawned.Count; i++) {
                if (liveEnemiesSpawned[i] == null) {
                    liveEnemiesSpawned.RemoveAt(i);
                    i--;
                }
            }
            if (liveEnemiesSpawned.Count == 0 && !SpawningEnemies) {
                waveTimer.EndWave(true);
            }
        }
    }

    public void Spawn() {
        var spawnVariance = new Vector3((Random.value - 0.5f) * EnemySpawnVariance.x, (Random.value - 0.5f) * EnemySpawnVariance.y);
        var newEnemy = (Enemy)Instantiate(EnemyPrefab, transform.position + spawnVariance, Quaternion.identity);
        var targetVariance = new Vector3((Random.value - 0.5f) * EnemyTargetVariance.x, (Random.value - 0.5f) * EnemyTargetVariance.y);
        newEnemy.TargetPosition = EnemyTarget.position + targetVariance;
        newEnemy.Health *= HealthModifier;
        newEnemy.Money = Mathf.FloorToInt(newEnemy.Money * MoneyModifier);
        liveEnemiesSpawned.Add(newEnemy);
        TimeToNextSpawn = EnemySpawnInterval;
    }
}