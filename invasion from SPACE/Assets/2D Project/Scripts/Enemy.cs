using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDiedFunc(float points);
    public static event EnemyDiedFunc OnEnemyDied;

    [Header("Enemy Settings")]
    public float scoreValue = 10f;
    private AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch!");
        
        // todo - destroy the bullet
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player Bullet Layer")){
            Destroy(collision.gameObject);
            OnEnemyDied.Invoke(10);
            Destroy(gameObject);
        }
        // todo - trigger death animation
    }
}
