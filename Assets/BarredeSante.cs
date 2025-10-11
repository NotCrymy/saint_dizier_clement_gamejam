using UnityEngine;
using UnityEngine.UI;

public class BarredeSante : MonoBehaviour
{
    [SerializeField] private Slider Sliderdesante;
    [SerializeField] private SystemeDeSante Systemdesante;
    [SerializeField] private float smoothSpeed = 0.1f;
    private float targetFillAmount = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Sliderdesante == null || Systemdesante == null) return;

        Sliderdesante.minValue = 0f;
        Sliderdesante.maxValue = 1f;
        Sliderdesante.value = 1f;

        // Initialize with current health
        float initialHealth = Systemdesante.ObtenirSanteNormalisee();
        targetFillAmount = Mathf.Clamp01(initialHealth);
        UpdateFillColor(initialHealth);

        Systemdesante.OnchangedSante += HandleHealthChanged;
    }

    // Update is called once per frame
    void Update()
    {
        Sliderdesante.value = Mathf.Lerp(Sliderdesante.value, targetFillAmount, Time.deltaTime * smoothSpeed);
    }

    private void HandleHealthChanged(float normalizedHealth)
    {
        targetFillAmount = Mathf.Clamp01(normalizedHealth);
        UpdateFillColor(normalizedHealth);
    }

    private void UpdateFillColor(float healthNormalized)
    {
        if (Sliderdesante.fillRect != null)
        {
            var fillImage = Sliderdesante.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                fillImage.color = Color.Lerp(Color.red, Color.green, healthNormalized);
            }
        }
    }
}