using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    [Header("Audio")]
    public AudioSource musicSource;
    public AudioClip musicTrack;
    public AudioClip deathSound;
    private int score = 0;
    private int hiscore = 0;
    private const int maxscore = 9999;
    private const string HiScoreKey = "HISCORE";
    void Start()
    {
       Enemy.OnEnemyDied += OnEnemyDied;
       hiscore = PlayerPrefs.GetInt(HiScoreKey,0);
       UpdateScoreText();
       UpdateHiScoreText();

       if (musicSource != null && musicTrack != null)
        {
            musicSource.clip = musicTrack;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    void onDestroy()
    {
        Enemy.OnEnemyDied -= OnEnemyDied;
    }

    void OnEnemyDied(float score)
    {
        if (deathSound != null && musicSource != null)
        {
            musicSource.PlayOneShot(deathSound);
        }
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
