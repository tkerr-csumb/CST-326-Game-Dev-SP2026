using UnityEngine;

public class Slot : MonoBehaviour
{
   void onTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with {other.gameObject.name}");
        
    }
}
