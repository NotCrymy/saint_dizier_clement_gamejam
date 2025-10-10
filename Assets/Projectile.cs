using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public string targetTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si le collider est un ennemi
        if (other.CompareTag(targetTag))
        {
            // Récupère le script de santé
            SystemeDeSante systemeDeSante = other.GetComponent<SystemeDeSante>();
            if (systemeDeSante != null)
            {
                systemeDeSante.TakeDamage(damage);
            }
        }
    }
}