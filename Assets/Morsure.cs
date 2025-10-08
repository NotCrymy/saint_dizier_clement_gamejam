using UnityEngine;

public class Morsure : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnCollisionEnter(Collision collision)
    {
    // Vérifie si le collider a un composant SystemedeSante
    SystemeDeSante systemedesante = collision.gameObject.GetComponent<SystemeDeSante>();
        if (systemedesante != null)
        {
            systemedesante.TakeDamage(10f); // Fait subir 10 points de dégats lors de la collision
            Debug.Log("Morsure");
        }
    }
}
