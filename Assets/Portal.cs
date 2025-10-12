using UnityEngine;

public class Portal : MonoBehaviour
{
    public enum TypePortail { Bonus, Malus }
    public TypePortail typePortail;
    public float moveSpeed = 3f;
    public float damageMultiplier = 2f;

    void Start()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int portalLayer = LayerMask.NameToLayer("Portals");
        Physics.IgnoreLayerCollision(portalLayer, enemyLayer, true);
    }

    void Update()
    {
        // Avance tout droit selon sa direction locale
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Attack attack = other.GetComponent<Attack>();
            if (attack != null)
            {
                if (typePortail == TypePortail.Bonus)
                    attack.ApplyDamageMultiplier(damageMultiplier);
                else
                    attack.ApplyDamageMultiplier(1f / damageMultiplier);
            }

            Destroy(gameObject);
        }
    }
}