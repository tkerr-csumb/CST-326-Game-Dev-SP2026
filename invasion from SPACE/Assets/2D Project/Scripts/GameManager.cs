using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;

    private int score = 0;
    private int hiscore = 0;
    private const int maxscore = 9999;
    private const string HiScoreKey = "HISCORE";
    void Start()
    {
       // todo - sign up for notification about enemy death 
       Enemy.OnEnemyDied += OnEnemyDied;
       hiscore = PlayerPrefs.GetInt(HiScoreKey,0);
       UpdateScoreText();
       UpdateHiScoreText();
    }

    void onDestroy()
    {
        Enemy.OnEnemyDied -= OnEnemyDied;
    }

    void OnEnemyDied(float score)
    {
        Debug.Log($"Killed enemy worth {score} points");
        AddScore(score);
    }

    private void AddScore(float points){
        score += Mathf.RoundToInt(points);
        score = Mathf.Clamp(score, 0, maxscore);
        UpdateScoreText();
        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetInt(HiScoreKey, hiscore);
            PlayerPrefs.Save();
            UpdateHiScoreText();
        }
    }

    private void UpdateScoreText(){
        scoreText.text = $"SCORE\n{score:D4}";
    }

    private void UpdateHiScoreText()
    {
        hiscoreText.text = $"HI-SCORE\n{hiscore:D4}";
    }
}
