using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Réglages du spawn")]
    public GameObject[] enemyPrefabs;  // Liste des types d'ennemis
    public float spawnInterval = 2f;   // Temps entre chaque spawn
    public int maxEnemies = 10;        // Nombre maximum en même temps
    public float spawnRadius = 5f;     // Zone autour du spawner

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Compter combien d'ennemis "Enemy" sont actuellement dans la scène
        int currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (currentEnemies >= maxEnemies) return;

        // Choisir un prefab aléatoire
        GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Choisir une position aléatoire autour du spawner
        Vector3 spawnPos = transform.position + (Random.insideUnitSphere * spawnRadius);
        spawnPos.y = transform.position.y;

        // Instancier
        GameObject newEnemy = Instantiate(prefabToSpawn, spawnPos, transform.rotation);

        // S'assurer qu'il a bien le tag "Enemy"
        newEnemy.tag = "Enemy";
    }
}
