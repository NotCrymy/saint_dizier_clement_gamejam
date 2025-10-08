using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Vector3 a = new Vector3(0, -1, 0);
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 force = rb.mass * a;
        rb.AddForce(force);
    }
}
