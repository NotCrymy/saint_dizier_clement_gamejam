using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject Projectile;
    public Transform Emitter;
    public float frequency;
    public int Number;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(Emitter.position, Vector, Color.red);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(BulletLaunching());
        }
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }


    IEnumerator BulletLaunching()
    {
        for (int i = 0; i < Number; i++)
        {
            GameObject bullet = Instantiate(Projectile, Emitter.position, Emitter.rotation);

            // ajoute un destroy automatique uniquement au clone :
            Destroy(bullet, 2f);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(-100f * Emitter.forward, ForceMode.Impulse);

            yield return new WaitForSeconds(frequency);
        }
    }
}