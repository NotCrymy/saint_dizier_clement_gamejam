using UnityEngine;
using System.Collections;

public class SystemeDeSante : MonoBehaviour
{
    public event System.Action<float> OnchangedSante;

    [SerializeField] private float maxSante = 100f;
    [SerializeField] private float actuelleSante;

    private Animator animator;
    private bool isDead = false;

    public float MaxSante => maxSante;
    public float ActuelleSante => actuelleSante;
    public bool IsDead => actuelleSante <= 0;

    private void Start()
    {
        actuelleSante = maxSante;
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetBool("IsDead", false);
        }

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
            animator.SetBool("IsDead", true);
        }

        if (CompareTag("Player"))
        {
            GameManager gm = Object.FindAnyObjectByType<GameManager>();
            // Mort du joueur
            gm.EndGame();

            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(HandleEnemyDeath());
        }
    }

    private IEnumerator HandleEnemyDeath()
    {
        // Désactive Poursuite
        foreach (var poursuite in GetComponents<Poursuite>())
        {
            poursuite.enabled = false;
        }

        // Désactive Morsure
        foreach (var morsure in GetComponents<Morsure>())
        {
            morsure.enabled = false;
        }

        ScoreManager scoreManager = Object.FindAnyObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddPoints(10);
        }

        yield return new WaitForSeconds(3.5f); 
        Destroy(gameObject);
    }

    public void SetHealth(float newMax, float newCurrent)
        {
            maxSante = newMax;
            actuelleSante = Mathf.Clamp(newCurrent, 0f, newMax);
            OnchangedSante?.Invoke(ObtenirSanteNormalisee());
        }
}