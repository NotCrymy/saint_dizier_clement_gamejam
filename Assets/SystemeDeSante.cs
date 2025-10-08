using UnityEngine;

public class SystemeDeSante : MonoBehaviour
{
    public event System.Action<float> OnchangedSante;
    [SerializeField] private float maxSante = 100f;
    [SerializeField] private float actuelleSante;

    public float MaxSante
    {
        get { return maxSante; }
    }

    public float ActuelleSante
    {
        get { return actuelleSante; }
    }

    public bool IsDead
    {
        get { return actuelleSante <= 0; }
    }

    private void Start()
    {
        // Initialise avec la santé maximale quand le jeu est lancé
        actuelleSante = maxSante;

        // Informe les autres classes à propos de la santé initiale
        if (OnchangedSante != null)
        {
            OnchangedSante(ObtenirSanteNormalisee());
        }
    }

    public float ObtenirSanteNormalisee()
    {
        return actuelleSante / maxSante;
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        actuelleSante = Mathf.Clamp(actuelleSante - damage, 0f, maxSante);
        Debug.Log($"{gameObject.name} took {damage} damage. Health: {actuelleSante}/{maxSante}");

        // Notifier les "listeners" de la modification de la santé
        if (OnchangedSante != null)
        {
            OnchangedSante(ObtenirSanteNormalisee());
        }

        if (actuelleSante <= 0)
        {
            // On peut placer ici la mort (ou faire une classe externe)
            Debug.Log($"{gameObject.name} est mort !");
            Destroy(gameObject, 5f);
        }
    }

    public void Heal(float amount)
    {
        actuelleSante = Mathf.Clamp(actuelleSante + amount, 0f, maxSante);
        Debug.Log($"{gameObject.name} healed for {amount}. Health: {actuelleSante}/{maxSante}");

        // Notifier listeners about health change
        if (OnchangedSante != null)
        {
            OnchangedSante(ObtenirSanteNormalisee());
        }
    }
}