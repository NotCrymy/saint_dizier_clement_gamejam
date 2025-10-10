using UnityEngine;

// script que j ai ajoute a mon prefab de bullet
public class Projectile : MonoBehaviour
{
    public float damage = 10f;

    public string targetTag = "Enemy"; // j ai ajoute ca pour tester les tags

    private void OnCollisionEnter(Collision collision)
    {
        // on check le tag
        if (collision.gameObject.CompareTag(targetTag))
        {
            // on update la sante avec l'objet que la balle collisionne 
            // (la mort est géré dans mon systeme de sante, quand elle est negative ou null, je destroy l'object)
            // j ai aussi enleve la de barre de sante sur les ennemies pour simplifier mon affichage mais cela ne changerai rien a la logique du code
            SystemeDeSante systemeDeSante = collision.gameObject.GetComponent<SystemeDeSante>();

            if (systemeDeSante != null)
            {
                systemeDeSante.TakeDamage(damage);
            }
        }
    }
}