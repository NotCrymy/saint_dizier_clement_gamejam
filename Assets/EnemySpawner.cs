using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Réglages du spawn")]
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 2f;   // intervalle initial
    public int maxEnemies = 10;        // max initial
    public float spawnRadius = 5f;

    [Header("Progression")]
    public float minSpawnInterval = 0.5f; // intervalle minimal
    public float spawnAcceleration = 0.01f; // combien on réduit l'intervalle par seconde
    public int maxEnemiesCap = 50; // nombre max global possible

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // Réduire progressivement l'intervalle
        spawnInterval -= spawnAcceleration * Time.deltaTime;
        if (spawnInterval < minSpawnInterval) spawnInterval = minSpawnInterval;

        // Optionnel : augmenter maxEnemies avec le temps
        if (maxEnemies < maxEnemiesCap)
        {
            maxEnemies = Mathf.Min(maxEnemiesCap, maxEnemies + Mathf.FloorToInt(Time.deltaTime * 0.1f));
        }

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
    }
}