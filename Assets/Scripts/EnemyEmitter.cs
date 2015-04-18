using UnityEngine;
using System.Collections;

public class EnemyEmitter : MonoBehaviour {

    public Enemy EnemyPrefab;
    public Vector2 EnemySpawnVariance;
    public Transform EnemyTarget;
    public Vector2 EnemyTargetVariance;

    [Range(0, 30)]
    public float EnemySpawnInterval;
    [Range(0, 30)]
    public float TimeToNextSpawn;
    [Range(0, 50)]
    public int InitialEnemiesToSpawn;

    void Start() {
        for (int i = 0; i < InitialEnemiesToSpawn; i++) {
            Spawn();
        }
    }

    void Update() {
        TimeToNextSpawn -= Time.deltaTime;
        if (TimeToNextSpawn <= 0) {
            Spawn();
        }
    }

    public void Spawn() {
        var spawnVariance = new Vector3((Random.value - 0.5f) * EnemySpawnVariance.x, (Random.value - 0.5f) * EnemySpawnVariance.y);
        var newEnemy = (Enemy)Instantiate(EnemyPrefab, transform.position + spawnVariance, Quaternion.identity);
        var targetVariance = new Vector3((Random.value - 0.5f) * EnemyTargetVariance.x, (Random.value - 0.5f) * EnemyTargetVariance.y);
        newEnemy.TargetPosition = EnemyTarget.position + targetVariance;
        TimeToNextSpawn = EnemySpawnInterval;
    }
}