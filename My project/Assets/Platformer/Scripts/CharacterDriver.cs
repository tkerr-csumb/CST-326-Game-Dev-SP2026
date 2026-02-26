using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterDriver : MonoBehaviour
{
    public float groundAcceleration = 15f;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float apexHeight = 4.5f;
    public float apexTime = 0.5f;
    public GameObject merrio;

    Vector2 _velocity;
    CharacterController _controller;
    Quaternion facingRight;
    Quaternion facingLeft;
    Animator _animator;
    void Awake(){
        _animator = GetComponent<Animator>();
        facingRight = Quaternion.Euler(0f, 90f, 0f);
        facingLeft = Quaternion.Euler(0f, 270f, 0f);
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update(){
        float direction = 0f;
        if (Keyboard.current.dKey.isPressed) direction += 1f;
        if (Keyboard.current.aKey.isPressed) direction -= 1f;
        bool jumpPressedThisFrame = Keyboard.current.wKey.wasPressedThisFrame;
        bool jumpHeld = Keyboard.current.wKey.isPressed;
        float gravityModifier = 1f;

        if (_controller.isGrounded){
            if (direction != 0f) {
                if(Mathf.Sign(direction) != Mathf.Sign(_velocity.x))
                    _velocity.x = 0f;
                _velocity.x += direction * groundAcceleration * Time.deltaTime;
                _velocity.x = Mathf.Clamp(_velocity.x, -walkSpeed, walkSpeed);
                transform.rotation = (direction > 0f) ? facingRight : facingLeft;
            } else {
                _velocity.x = Mathf.MoveTowards(_velocity.x, 0f, groundAcceleration * Time.deltaTime);
            }

            if (jumpPressedThisFrame){
                _velocity.y = 2f * apexHeight / apexTime;
            }
        } else {
            if (!jumpHeld)
                gravityModifier = 2f;
        }
        float gravity = 2f * apexHeight / (apexTime* apexTime);
        _velocity.y -= gravity * gravityModifier * Time.deltaTime;

        float deltaX = _velocity.x * Time.deltaTime;
        float deltaY = _velocity.y * Time.deltaTime;

        Vector3 deltaPosition = new(deltaX, deltaY, 0f);
        CollisionFlags collisions = _controller.Move(deltaPosition);

        if ((collisions & CollisionFlags.CollidedAbove) != 0){
            _velocity.y = -1f;
            RaycastHit hit;
            Vector3 rayOrigin = transform.position + Vector3.up * (_controller.height/2f - _controller.radius);
            float rayLength = _controller.radius + 2f;

            if (Physics.Raycast(transform.position, Vector3.up, out hit, rayLength)){
                Debug.DrawRay(rayOrigin, Vector3.up * rayLength, Color.red, 2f);
                if (hit.collider.gameObject.CompareTag("Brick")){
                    Destroy(hit.collider.gameObject);
                    Score.Instance.AddScore(100);
                }
                if (hit.collider.gameObject.CompareTag("QBlock"))
                {
                    CoinCounter.Instance.AddCoin(1);
                    Score.Instance.AddScore(100);
                }
            }
        }

        if ((collisions & CollisionFlags.CollidedSides) != 0)
            _velocity.x = 0f;


        _animator.SetFloat("Speed", Mathf.Abs(_velocity.x));
        _animator.SetBool("Grounded", _controller.isGrounded);
    }

    void OnControllerColliderHit(ControllerColliderHit hit){
        if (hit.collider.gameObject.CompareTag("KILL")){
            Debug.Log("YOU LOSE");
            Destroy(gameObject);
        }
        if (hit.collider.gameObject.CompareTag("WIN")){
            Debug.Log("YOU WIN");
        }
    }
}
