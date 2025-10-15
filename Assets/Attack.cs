using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public GameObject Projectile;
    public Transform Emitter;
    public float frequency = 0.5f;
    public int Number = 1;

    [Header("Dégâts")]
    public float baseDamage = 20f;
    public float damageMultiplier = 1f;

    private Coroutine shootingCoroutine = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (shootingCoroutine == null)
                shootingCoroutine = StartCoroutine(BulletLaunching());
            else
            {
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
        while (true)
        {
            for (int i = 0; i < Number; i++)
            {
                GameObject bullet = Instantiate(Projectile, Emitter.position, Emitter.rotation);
                
                Projectile projectileScript = bullet.GetComponent<Projectile>();
                if (projectileScript != null)
                    projectileScript.damage = baseDamage * damageMultiplier;

                Destroy(bullet, 2f);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddForce(-100f * Emitter.forward, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(frequency);
        }
    }

    public void ApplyDamageMultiplier(float multiplier)
    {
        damageMultiplier *= multiplier;
        Debug.Log($"Nouveau multiplicateur de dégâts : x{damageMultiplier}");
    }
}