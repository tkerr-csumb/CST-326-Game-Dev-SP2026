using UnityEngine;
using UnityEngine.InputSystem;

public class DemoPaddle : MonoBehaviour
{
    public float paddleSpeed = 1f;
    public float maxZ = 6f;
    public float minZ = -6f;
    private Rigidbody rBody;
    public bool isRightPaddle = false;
    public bool isLeftPaddle = false;
    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.isKinematic = true;
        rBody.useGravity = false;
        rBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }

    // Update is called once per frame
    float input;

void Update()
{
    input = 0f;

    if (isRightPaddle)
    {
        if (Keyboard.current.wKey.isPressed) input = 1f;
        else if (Keyboard.current.sKey.isPressed) input = -1f;
    }

    if (isLeftPaddle)
    {
        if (Keyboard.current.oKey.isPressed) input = 1f;
        else if (Keyboard.current.lKey.isPressed) input = -1f;
    }
}

void FixedUpdate()
{
    if (input == 0f)
        return;

    BoxCollider box = GetComponent<BoxCollider>();
    float halfPaddle = box.bounds.extents.z;

    Vector3 newPos = rBody.position;
    newPos.z += input * paddleSpeed * Time.fixedDeltaTime;
    newPos.z = Mathf.Clamp(newPos.z, minZ + halfPaddle, maxZ - halfPaddle);

    rBody.MovePosition(newPos);
}
}
