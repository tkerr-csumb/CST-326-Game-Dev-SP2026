using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public AudioSource bulletSource;
    public AudioClip bulletClip;
    public GameObject bulletPrefab;
    public Transform shootOffsetTransform;
    public float moveSpeed = 5f;

    void Start()
    {
        // todo - get and cache animator
    }
    
    void Update()
    {
        if (Keyboard.current == null) return;
        float move = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            move = -1f;
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            move = 1f;
        }
        transform.position += Vector3.right * move * moveSpeed * Time.deltaTime;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameObject shot = Instantiate(bulletPrefab, shootOffsetTransform.position, Quaternion.identity);
            bulletSource.PlayOneShot(bulletClip);
            Debug.Log("Bang!");
            Destroy(shot, 3f);
            GetComponent<Animator>().SetTrigger("Shot Trigger");
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet Layer")){
            Destroy(collision.gameObject);
            Destroy(gameObject);
            SceneManager.LoadScene("Credits");
        }
    }
}
