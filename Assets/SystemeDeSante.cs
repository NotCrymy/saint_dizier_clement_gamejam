using UnityEngine;
using System.Collections;

public class SystemeDeSante : MonoBehaviour
{
    public event System.Action<float> OnchangedSante;

    [SerializeField] private float maxSante = 100f;
    [SerializeField] private float actuelleSante;
    public Canvas deathCanvas;

    private Animator animator;
    private bool isDead = false;

    public float MaxSante => maxSante;
    public float ActuelleSante => actuelleSante;
    public bool IsDead => actuelleSante <= 0;

    private void Start()
    {
        actuelleSante = maxSante;
        animator = GetComponent<Animator>();

        // Assurer que le zombie démarre sur l'animation Walking
        if (animator != null)
        {
            animator.SetBool("IsDead", false);
        }

        // Informe les autres classes à propos de la santé initiale
        OnchangedSante?.Invoke(ObtenirSanteNormalisee());
    }

    public float ObtenirSanteNormalisee()
    {
        return actuelleSante / maxSante;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        actuelleSante = Mathf.Clamp(actuelleSante - damage, 0f, maxSante);
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {actuelleSante}/{maxSante}");

        OnchangedSante?.Invoke(ObtenirSanteNormalisee());

        if (actuelleSante <= 0)
        {
            Mourir();
        }
    }

    private void Mourir()
    {
        if (isDead) return;
        isDead = true;
                
        Debug.Log($"{gameObject.name} est mort !");

        if (animator != null)
        {
            // Arrêter la marche et lancer la mort
            animator.SetBool("IsDead", true);
        }

        if (CompareTag("Player"))
        {
            // Mort du joueur
            if (deathCanvas != null)
                deathCanvas.gameObject.SetActive(true);

            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(HandleEnemyDeath());
        }
    }

private IEnumerator HandleEnemyDeath()
    {
        // Stop tous les scripts de type Poursuite pour geler le zombie
        foreach (var comp in GetComponents<MonoBehaviour>())
        {
            if (comp != this && comp is Poursuite)
                comp.enabled = false;
        }

        // Ajouter des points
        ScoreManager scoreManager = Object.FindAnyObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddPoints(10); // par exemple 10 points par zombie
        }

        // attendre la fin du clip avant destruction
        yield return new WaitForSeconds(3.5f); 
        Destroy(gameObject);
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        actuelleSante = Mathf.Clamp(actuelleSante + amount, 0f, maxSante);
        Debug.Log($"{gameObject.name} healed for {amount}. Health: {actuelleSante}/{maxSante}");

        OnchangedSante?.Invoke(ObtenirSanteNormalisee());
    }
}