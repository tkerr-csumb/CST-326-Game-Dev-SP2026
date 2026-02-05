using UnityEngine;
using TMPro;

public class bouncingWeasel : MonoBehaviour
{
    public Rigidbody rb;
    public float weaselSpeed;
    public float speedMultiplier = 1.15f;
    public float minBounceAngle = 30f;
    public float spinSpeed;
    public float wallBounceDistance = 1f;
    public float paddleStun = 0.2f;
    public float wallStun = 0.1f;
    float lastPaddleHit = -1f;
    float lastWallHit = -1f;
    public int leftScore;
    public int rightScore;
    public TMP_Text leftScoreText;
    public TMP_Text rightScoreText;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 0.1f;
    }

    void Start()
    {
        UpdateScoreUI();
        ResetAndLauncher();
    }

    void ResetAndLauncher()
    {
        rb.linearVelocity = Vector3.zero;
        transform.position = Vector3.zero;
        lastPaddleHit = -1f;
        lastWallHit = -1f;

        bool startRight = Random.value >= 0.5f;
        float xDirect = startRight ? -1f : 1f;
        float zDirect = Random.Range(-1f, 1f);

        rb.linearVelocity =
            new Vector3(xDirect, 0f, zDirect).normalized * weaselSpeed;
    }

    void Update(){
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }
    void FixedUpdate(){
        if (rb.linearVelocity.magnitude < weaselSpeed && rb.linearVelocity.magnitude > 0f){
            rb.linearVelocity = rb.linearVelocity.normalized * weaselSpeed;
        }
        if (Time.time - lastWallHit < wallStun) return;

        RaycastHit rayOfLight;
        Vector3 velocity = rb.linearVelocity;

        if (velocity.magnitude > 0.1f){
            if (Physics.Raycast(transform.position, velocity.normalized, out rayOfLight, wallBounceDistance)){
                if (rayOfLight.collider.CompareTag("Wall")){
                    bounceOffWall(rayOfLight.normal);
                    lastWallHit = Time.time;
                }
            }
        }
    }
    void bounceOffPaddle(Collision collision){
        ContactPoint contact = collision.contacts[0];

        float paddleZ = collision.transform.position.z;
        float hitZ = contact.point.z;

        BoxCollider box = collision.collider as BoxCollider;
        float half = box.bounds.extents.z;

        float offset = Mathf.Clamp((hitZ - paddleZ) / half, -1f, 1f);

        float xDirect = contact.normal.x > 0 ? 1f : -1f;

        Vector3 newVelocity = new Vector3(xDirect,0f,offset);

        float angle = Vector3.Angle(newVelocity, new Vector3(xDirect, 0f, 0f));
        if (angle < minBounceAngle)
        {
            float minZ =
                Mathf.Abs(xDirect) * Mathf.Tan((90f - minBounceAngle) * Mathf.Deg2Rad);
            newVelocity.z = Mathf.Sign(offset) * minZ;
        }

        float speed = Mathf.Max(rb.linearVelocity.magnitude * speedMultiplier, weaselSpeed);
        rb.linearVelocity = newVelocity.normalized * speed;
    }
    void bounceOffWall(Vector3 normal){
        Vector3 reflected = Vector3.Reflect(rb.linearVelocity, normal);
        float speed = Mathf.Max(reflected.magnitude * speedMultiplier, weaselSpeed);
        rb.linearVelocity = reflected.normalized * speed;
    }
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Wall")){
            if (Time.time - lastWallHit < wallStun) return;

            bounceOffWall(collision.contacts[0].normal);
            lastWallHit = Time.time;
            return;
        }
        if (collision.gameObject.CompareTag("Paddle")){
            if (Time.time - lastPaddleHit < paddleStun) return;

            bounceOffPaddle(collision);
            lastPaddleHit = Time.time;
        }
    }  

    void UpdateScoreUI(){
        if (leftScoreText != null)
            leftScoreText.text = leftScore.ToString();
        if (rightScoreText != null)
            rightScoreText.text = rightScore.ToString();
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("LeftGoal")){
            rightScore++;
            UpdateScoreUI();
            Debug.Log("Right player scored!");
            ResetAndLauncher();
        }
        if (other.CompareTag("RightGoal")){
            leftScore++;
            UpdateScoreUI();
            Debug.Log("Left player scored!");
            ResetAndLauncher();
        }
    }
}
