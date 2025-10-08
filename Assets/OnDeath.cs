using UnityEngine;
using UnityEngine.UI;

public class OnDeath : MonoBehaviour
{
    public SystemeDeSante Sante; //de quel GameObject suit on les points de vie
    public Canvas deathCanvas; //une image qui sera affichée en cas de mort du GameObject

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Sante != null && Sante.IsDead)
        {
            udied();
            displayudied();
        }
    }

    private void udied()
    {
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
        }
        Destroy(gameObject, 5f);
    }

    private void displayudied()
    {
        if (deathCanvas != null)
        {
            deathCanvas.gameObject.SetActive(true);
        }
    }
}
