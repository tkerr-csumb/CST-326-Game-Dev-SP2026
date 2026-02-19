using UnityEngine;

public class QuestionBlock : MonoBehaviour{
    public void AddCoin(){
        CoinCounter.Instance.AddCoin(1);
    }
}
