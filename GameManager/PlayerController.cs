using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    float _vy = 0f;
    bool _grounded = true;
    const float gravity = -9.8f;
    const float jumpForce = 5f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _vy = jumpForce;
            _grounded = false;
        }
        _vy += gravity * Time.deltaTime;

        transform.Translate(new Vector3(h* speed * Time.deltaTime, _vy * Time.deltaTime, v* speed * Time.deltaTime), Space.World);

        if (transform.position.y <= 0.5f)
        {
            transform.position = new Vector3( transform.position.x, 0.5f, transform.position.z);
            _grounded = true;
        }
        if (transform.position.x < -5f || transform.position.x > 5f || transform.position.z < -7.5f)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
        }
    }
}
