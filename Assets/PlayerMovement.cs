using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float strafeSpeed = 8f; // vitesse latérale
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // évite que le joueur penche
    }

    void FixedUpdate()
    {
        HandleStrafe();
    }

    void HandleStrafe()
    {
        float move = 0f;

        if (Input.GetKey(KeyCode.A)) // gauche
        {
            move = 1f;
        }
        else if (Input.GetKey(KeyCode.D)) // droite
        {
            move = -1f;
        }

        // Déplacement fluide latéral via physique
        Vector3 movement = transform.right * move * strafeSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}