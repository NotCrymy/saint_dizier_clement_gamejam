using UnityEngine;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; 
    private int score = 0;

    void Start()
    {
        UpdateUI();
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score : {score}";
    }
}
