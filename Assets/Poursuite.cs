using UnityEngine;

public class Poursuite : MonoBehaviour
{
    [Header("Cible à poursuivre")]
    public Transform cible;

    [Header("Paramètres de mouvement")]
    public float vitesse = 2f;
    public float rotationVitesse = 5f;
    public float distanceArret = 1.2f;

    void Update()
    {
        if (cible == null) return;

        // Direction vers la cible
        Vector3 direction = (cible.position - transform.position);
        direction.y = 0f; // Ne pas incliner vers le haut ou le bas

        float distance = direction.magnitude;

        // Rotation progressive vers la cible
        if (direction != Vector3.zero)
        {
            Quaternion rotationVoulue = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationVoulue, rotationVitesse * Time.deltaTime);
        }

        // Déplacement seulement si la cible n’est pas trop proche
        if (distance > distanceArret)
        {
            transform.position += transform.forward * vitesse * Time.deltaTime;
        }
    }
}
