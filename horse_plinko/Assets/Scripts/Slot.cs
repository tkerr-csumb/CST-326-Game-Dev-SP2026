using UnityEngine;

public class Slot : MonoBehaviour
{
    public int slotnumber;
   void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Slot #{slotnumber} with {other.gameObject.name}");

    }
}
