using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public TextMeshProUGUI scoreText;
    int score = 0;
    void Awake()
    {
        Instance = this;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = $"Mario\n" + score;
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }
}
