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

        Vector3 direction = (cible.position - transform.position);
        direction.y = 0f;

        float distance = direction.magnitude;

        if (direction != Vector3.zero)
        {
            Quaternion rotationVoulue = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationVoulue, rotationVitesse * Time.deltaTime);
        }

        if (distance > distanceArret)
        {
            transform.position += transform.forward * vitesse * Time.deltaTime;
        }
    }
}
