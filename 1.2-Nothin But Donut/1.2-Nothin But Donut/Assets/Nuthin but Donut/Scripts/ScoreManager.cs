using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScoreManager : MonoBehaviour
{
    [Header("references")]
    public TextMeshProUGUI scoreText;
    public AudioClip crowdCheering;

    AudioSource audioSource;
    int score = -1000;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AddScore()
    {
        score += 100;

        string scoreString = $"Student Debt: {score}";
        scoreText.text = scoreString;
        // Todo
        // 1. update the text to change based on the new score
        // 2. play a sound for the crowd cheering
        audioSource.PlayOneShot(crowdCheering);
    }
}
