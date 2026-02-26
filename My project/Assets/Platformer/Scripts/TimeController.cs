using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public float timeLeft = 100;
    public GameObject character;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){     
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft == 0){
            Debug.Log($"Time's up!\n GAME OVER");
        } else if (timeLeft < 0) {
            Destroy(character);
        } else {
            timeLeft -= Time.deltaTime;
            timeText.text = $"TIME\n {((int)timeLeft).ToString()}";
        }
    }
}
