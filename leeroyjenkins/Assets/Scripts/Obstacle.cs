using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float rotateSpeed = 45f;  // degrees per second

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
