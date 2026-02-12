using UnityEngine;

public enum PowerUpType{
    EnlargePaddle,
    SlowDownBall
}
public class PowerUp : MonoBehaviour{
    public PowerUpType type;
    public float duration = 5f;

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Ball")) return;
        Paddle[] paddles = FindObjectsOfType<Paddle>();
        Paddle closest = null;
        float minDist = float.MaxValue;

        foreach (var paddle in paddles){
            float dist = Vector3.Distance(paddle.transform.position, transform.position);
            if (dist < minDist){
                minDist = dist;
                closest = paddle;
            }
        }

        if (closest != null){
            closest.ApplyPowerUpScale(1.5f, 5f);
        }
        Destroy(gameObject);
    }

    void ApplyPowerUp(GameObject ball){
        Paddle paddle = FindObjectOfType<Paddle>();

        switch (type){
            case PowerUpType.EnlargePaddle:
                paddle.StartCoroutine(paddle.PowerUpScale(1.5f, 5f));
                break;
            case PowerUpType.SlowDownBall:
                ball.GetComponent<Rigidbody>().linearVelocity *= 0.5f;
                break;
        }
    }
}
