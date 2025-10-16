using UnityEngine;
using TMPro; // Nécessaire pour TextMeshPro

public class Portal : MonoBehaviour
{
    public enum TypePortail { Bonus, Malus }
    public TypePortail typePortail;
    public float moveSpeed = 3f;
    public float damageMultiplier = 1f;

    [Header("UI")]
    public TextMeshPro multiplierText;

    void Start()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        int portalLayer = LayerMask.NameToLayer("Portals");
        Physics.IgnoreLayerCollision(portalLayer, enemyLayer, true);

        UpdateText();
    }

    void Update()
    {
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

    public void ModifyDamageMultiplier(float amount)
    {
        float step = amount * 0.01f;

        if (typePortail == TypePortail.Bonus)
            damageMultiplier += step; // augmente
        else
            damageMultiplier -= step; // diminue

        UpdateText();
    }

    private void UpdateText()
    {
        if (multiplierText != null && typePortail == TypePortail.Bonus)
            multiplierText.text = $"x{damageMultiplier:F2}";
        if (multiplierText != null && typePortail == TypePortail.Malus)
            multiplierText.text = $"x{(1f / damageMultiplier):F2}";
    }
}