using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject healthBarPrefab;
    public float heightOffset = 2f;
    public float smoothSpeed = 5f;

    private Slider slider;
    private GameObject barInstance;
    private SystemeDeSante systemeDeSante;
    private float targetValue = 1f;

    void Start()
    {
        systemeDeSante = GetComponent<SystemeDeSante>();
        if (systemeDeSante == null || healthBarPrefab == null) return;

        barInstance = Instantiate(healthBarPrefab, transform.position + Vector3.up * heightOffset, Quaternion.identity);
        slider = barInstance.GetComponentInChildren<Slider>();
        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = systemeDeSante.ObtenirSanteNormalisee();
        }

        systemeDeSante.OnchangedSante += UpdateHealth;
    }

    void Update()
    {
        if (barInstance != null)
        {
            // Suivre le zombie
            barInstance.transform.position = transform.position + Vector3.up * heightOffset;
            barInstance.transform.LookAt(Camera.main.transform);

            if (slider != null)
            {
                slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * smoothSpeed);
            }
        }
    }

    private void UpdateHealth(float normalizedHealth)
    {
        targetValue = normalizedHealth;

        if (slider != null)
        {
            // Color lerp green -> red
            var fillImage = slider.fillRect.GetComponent<Image>();
            if (fillImage != null)
                fillImage.color = Color.Lerp(Color.red, Color.green, normalizedHealth);
        }

        // cacher le slider quand à 0
        if (barInstance != null)
            barInstance.SetActive(normalizedHealth > 0f);
    }

    private void OnDestroy()
    {
        if (systemeDeSante != null)
            systemeDeSante.OnchangedSante -= UpdateHealth;

        if (barInstance != null)
            Destroy(barInstance);
    }
}