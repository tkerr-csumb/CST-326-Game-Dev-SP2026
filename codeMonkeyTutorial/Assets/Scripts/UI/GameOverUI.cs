using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour{
   [SerializeField] private TextMeshProUGUI recipesDeliveredText;

   private void Start() {
        GameHandler.Instance.OnStateChanged += GameHandler_OnStateChanged;
        Hide();
    }

    private void GameHandler_OnStateChanged(object sender, System.EventArgs e) {
        if (GameHandler.Instance.IsGameOver()) {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
