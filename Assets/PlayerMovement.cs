using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Paramètres")]
    public float strafeSpeed = 8f;

    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // empêche le joueur de pencher

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        HandleStrafe();
    }

    void HandleStrafe()
    {
        float strafeValue = 1.5f; // Default → Idle
        float move = 0f;           // décalage réel pour le Rigidbody

        // Détecte les touches
        if (Input.GetKey(KeyCode.A))
        {
            strafeValue = 3.5f;  // LeftStrafe
            move = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            strafeValue = 2.5f;  // RightStrafe
            move = 1f;
        }

        // Déplacement latéral via Rigidbody
        Vector3 movement = transform.right * move * strafeSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Mettre à jour l'Animator
        if (animator != null)
        {
            animator.SetFloat("StrafeDirection", strafeValue);
        }
    }
}