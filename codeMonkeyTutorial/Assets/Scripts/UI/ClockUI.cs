using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour{
    [SerializeField] private Image timerImage;

    private void Update() {
        timerImage.fillAmount = GameHandler.Instance.GetGamePlayingTimerNormalized();
    }
}
