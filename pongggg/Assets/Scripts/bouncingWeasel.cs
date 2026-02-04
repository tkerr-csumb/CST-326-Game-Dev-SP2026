using UnityEngine;

public class bouncingWeasel : MonoBehaviour
{
    public Rigidbody rb;
    public float weaselSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool startRight = UnityEngine.Random.value >= 0.5;
        float velocityX = 1f;
        if (startRight == true)
        {
            velocityX = -1f;
        }
        float velocityZ = UnityEngine.Random.Range(-1,1);
        rb.linearVelocity = new Vector3(velocityX * weaselSpeed, 0f, velocityZ * weaselSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
