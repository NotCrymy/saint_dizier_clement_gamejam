using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public string targetTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            SystemeDeSante systemeDeSante = other.GetComponent<SystemeDeSante>();
            if (systemeDeSante != null)
            {
                systemeDeSante.TakeDamage(damage);
            }
        }
    }
}