using UnityEngine;
using UnityEngine.InputSystem;

public class DemoPaddle : MonoBehaviour
{
    public float paddleSpeed = 1f;
    public float forceStrength = 10f;
    public float maxZ = 5f;
    public float minZ = -5f;
    private Rigidbody rBody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.isKinematic = true;
        rBody.useGravity = false;
        rBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            //Vector3 force = new Vector3(0f, 0f, forceStrength);
            //rBody.AddForce(force, ForceMode.Force);
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            //boxCollider.bounds.
            Vector3 newPosition = transform.position + new Vector3(0f, 0f, paddleSpeed) * Time.deltaTime;
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
            transform.position = newPosition;

            //transform.position += new Vector3(0f, 0f, paddleSpeed) * Time.deltaTime;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            //Vector3 force = new Vector3(0f, 0f, -forceStrength);
            //Rigidbody rBody = GetComponent<Rigidbody>();
            //rBody.AddForce(force, ForceMode.Force);
            //transform.position -= new Vector3(0f, 0f, paddleSpeed) * Time.deltaTime;
        }
    }
    void FixedUpdate(){
        float angle = 50f;

        //Vector3 up = Vector3.up;
        //Quaternion testRotation = Quarternion.Euler(60f,0f,0f);
        //Vector3 rotatedVector = testRotation * up;
        //Quarternion otherRotation = Quaternion.Euler(-60f,0f,0f);
        //Vector3 otherRotatedVector = otherRotation * up;

        //Quaternion someOtherAngleRotation = Quaternion.Euler(angle, 0f, 0f);
        //Vector3 otherRotatedVector = someOtherAngleRotation * up;
    }
}
