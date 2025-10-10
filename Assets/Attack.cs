using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public GameObject Projectile;
    public Transform Emitter;
    public float frequency = 0.5f; // temps entre les tirs
    public int Number = 1;          // nombre de projectiles par "salve"

    private Coroutine shootingCoroutine = null; // référence pour stopper la coroutine

    void Update()
    {
        // Quand on appuie sur Ctrl
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (shootingCoroutine == null)
            {
                // Démarre le tir
                shootingCoroutine = StartCoroutine(BulletLaunching());
            }
            else
            {
                // Stop le tir
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
        }
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator BulletLaunching()
    {
        while (true) // tir automatique tant que la coroutine tourne
        {
            for (int i = 0; i < Number; i++)
            {
                GameObject bullet = Instantiate(Projectile, Emitter.position, Emitter.rotation);
                Destroy(bullet, 2f);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddForce(100f * Emitter.forward, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(frequency);
        }
    }
}