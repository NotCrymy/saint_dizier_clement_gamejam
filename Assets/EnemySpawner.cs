using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Réglages du spawn")]
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 2f;
    public int maxEnemies = 10;
    public float spawnRadius = 5f;

    [Header("Progression")]
    public float minSpawnInterval = 0.5f;
    public float spawnAcceleration = 0.01f;
    public int maxEnemiesCap = 70;

    [Header("Scaling des ennemis")]
    public float speedIncrementPerSecond = 0.05f;   // vitesse supplémentaire par seconde
    public float healthIncrementPerSecond = 1f;     // points de vie supplémentaires par seconde

    private float timer = 0f;
    private float timeElapsed = 0f;

    void Update()
    {
        float delta = Time.deltaTime;
        timer += delta;
        timeElapsed += delta;

        spawnInterval -= spawnAcceleration * delta;
        if (spawnInterval < minSpawnInterval) spawnInterval = minSpawnInterval;

        if (maxEnemies < maxEnemiesCap)
            maxEnemies = Mathf.Min(maxEnemiesCap, maxEnemies + Mathf.FloorToInt(delta * 0.1f));

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currentEnemies >= maxEnemies) return;

        GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Vector3 spawnPos = transform.position + (Random.insideUnitSphere * spawnRadius);
        spawnPos.y = transform.position.y;

        GameObject newEnemy = Instantiate(prefabToSpawn, spawnPos, transform.rotation);
        newEnemy.tag = "Enemy";

        Poursuite poursuite = newEnemy.GetComponent<Poursuite>();
        SystemeDeSante sante = newEnemy.GetComponent<SystemeDeSante>();

        if (poursuite != null)
        {
            poursuite.vitesse += speedIncrementPerSecond * Time.time; 
        }

        if (sante != null)
        {
            sante.Heal(healthIncrementPerSecond * Time.time);
        }
    }
}