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
        // Assure que le panel Game Over est désactivé au début
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

        // Affiche le panel Game Over
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Met à jour le score final
        if (scoreManager != null && finalScoreText != null)
            finalScoreText.text = $"Score final : {scoreManager.getPoints()}";

        // Met à jour le temps de jeu
        if (timerText != null)
            timerText.text = $"Temps de jeu : {Mathf.FloorToInt(elapsedTime)}s";

        // Ensuite, stoppe le temps du jeu
        Time.timeScale = 0f;
    }

}