using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Game Over")]
    public GameObject gameOverPanel;     // Panel à afficher à la fin
    public TMP_Text finalScoreText;      // Texte pour afficher le score final
    public TMP_Text timerText;           // Texte pour afficher le temps de jeu

    [Header("Références")]
    public ScoreManager scoreManager;    // Référence à ton ScoreManager

    private float elapsedTime = 0f;      // Temps écoulé depuis le début du jeu
    private bool gameEnded = false;

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (!gameEnded)
            elapsedTime += Time.deltaTime;
    }

    public void EndGame()
    {
        gameEnded = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (scoreManager != null && finalScoreText != null)
            finalScoreText.text = $"Score final : {scoreManager.getPoints()}";

        if (timerText != null)
            timerText.text = $"Temps de jeu : {Mathf.FloorToInt(elapsedTime)}s";

        Time.timeScale = 0f;
    }

}