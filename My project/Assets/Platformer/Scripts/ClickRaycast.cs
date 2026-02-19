using UnityEngine;
using UnityEngine.InputSystem;

public class ClickRaycast : MonoBehaviour{
    // Update is called once per frame
    void Update(){
        if (Mouse.current.leftButton.wasPressedThisFrame){
            Ray rayOfLight = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(rayOfLight, out hit)){
                Debug.Log("Hit: " + hit.collider.name);

                BrickBreaker brick = hit.collider.GetComponent<BrickBreaker>();
                if (brick != null){
                    brick.Break();
                    return;
                }

                QuestionBlock qb = hit.collider.GetComponent<QuestionBlock>();
                if (qb != null){
                    qb.AddCoin();
                }
            }
        }
    }
}
