using UnityEngine;

public class Poursuite : MonoBehaviour
{
    public GameObject Cible;
    public float v = 2f; // vitesse

    void Update()
    {
        // C(t) = pos de l’objet
        Vector3 c = transform.position;

        // S(t) = pos de la cible
        Vector3 s = Cible.transform.position;

        // (S(t) - C(t) / norm de S-C)
        Vector3 dirNorm = (s - c).normalized;

        // C(t + delta t) = C(t) + v * dirNorm * delata t
        transform.position = c + dirNorm * v * Time.deltaTime;
    }
}