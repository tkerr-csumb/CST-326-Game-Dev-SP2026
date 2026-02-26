using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public float fixedY = 7.5f;
    public float fixedZ = -10f;

    void LateUpdate()
    {
        if (target == null) return;
        float smoothedX = Mathf.Lerp(transform.position.x, target.position.x, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedX,fixedY,fixedZ);
    }
}
