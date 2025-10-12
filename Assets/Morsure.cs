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
    SystemeDeSante systemedesante = collision.gameObject.GetComponent<SystemeDeSante>();
        if (systemedesante != null && collision.gameObject.CompareTag("Player"))
        {
            systemedesante.TakeDamage(10f); 
            Debug.Log("Morsure");
        }
    }
}
