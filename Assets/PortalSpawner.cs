using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    [Header("Réglages de spawn")]
    public GameObject[] portalPrefabs; // Liste des portails (bonus / malus)
    public float minSpawnInterval = 5f; // intervalle minimum
    public float maxSpawnInterval = 15f; // intervalle maximum

    private float timer = 0f;
    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawnTime();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextSpawnTime)
        {
            timer = 0f;
            SpawnPortal();
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnPortal()
    {
        if (portalPrefabs == null || portalPrefabs.Length == 0)
            return;

        GameObject prefab = portalPrefabs[Random.Range(0, portalPrefabs.Length)];

        GameObject newPortal = Instantiate(prefab, transform.position, transform.rotation);

        Destroy(newPortal, 30f); // Détruire après 30 secondes
    }
}