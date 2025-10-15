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
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        HandleStrafe();
    }

    void HandleStrafe()
    {
        float strafeValue = 1.5f;
        float move = 0f; 

        // Détecte les touches
        if (Input.GetKey(KeyCode.A))
        {
            strafeValue = 3.5f;
            move = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            strafeValue = 2.5f;
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