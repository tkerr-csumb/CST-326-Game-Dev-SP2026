using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CoinCounter : MonoBehaviour
{
    public static CoinCounter Instance;
    public TextMeshProUGUI coinText;
    public float piggyBank = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        Instance = this;
    }

    public void AddCoin(int coin){
        piggyBank += coin;
        coinText.text = $"Coins:\nX{((int)piggyBank).ToString()}";
    }
}
