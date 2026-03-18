using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 8, -6);
    public Vector3 lookOffset = new Vector3(0, 1.5f, 0);

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target.position + lookOffset);
        }
    }
}