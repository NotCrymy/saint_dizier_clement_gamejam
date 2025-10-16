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
        if (other.CompareTag("PortalBonus") || other.CompareTag("PortalMalus"))
        {
            Portal portal = other.GetComponent<Portal>();
            if (portal != null)
            {
                portal.ModifyDamageMultiplier(2f);
            }

            Destroy(gameObject);
        }
    }
    
}