using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class AgentController : MonoBehaviour
{
    public enum MouseButton {Left, Right};
    public Transform destinationMarker;
    public MouseButton mouseButton;
    private UnityEngine.AI.NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update(){
        ButtonControl buttonControl = (mouseButton == MouseButton.Left) ? Mouse.current.leftButton : Mouse.current.rightButton;
        if (buttonControl.wasPressedThisFrame){
            Ray mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.value);
            if (Physics.Raycast(mouseRay, out RaycastHit hitInfo)) {
                destinationMarker.position = hitInfo.point;
                _agent.SetDestination(destinationMarker.position);
            }
        }
    }
}
