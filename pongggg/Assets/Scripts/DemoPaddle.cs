using UnityEngine;
using Unityengine.InputSystem;

public class DemoPaddle : MonoBehaviour
{
    public float paddleSpeed = 1f;
    public float forceStrength = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            //Vector3 force = new Vector3(0f, 0f, forceStrength);
            //Rigidbody rBody = GetComponent<RigidBody>();
            //rBody.AddForce(force, ForceMode.Force);
            transform.position += new Vector3(0f, 0f, paddleSpeed) * Time.deltaTime;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            //Vector3 force = new Vector3(0f, 0f, -forceStrength);
            //Rigidbody rBody = GetComponent<RigidBody>();
            //rBody.AddForce(force, ForceMode.Force);
            transform.position -= new Vector3(0f, 0f, paddleSpeed) * Time.deltaTime;
        }
    }
}
