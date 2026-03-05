using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    [Header("Setup")]
    public GameObject[] enemyPrefabs;
    public int rows = 4;
    public int cols = 10;
    public float spacingX = 1.5f;
    public float spacingY = 1.0f;

    [Header("Movement")]
    public float stepInterval= 0.5f;
    public float moveStep = 0.5f;
    private int direction = 1;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float shootInterval = 2f;
    public float bulletSpeed = 5f;

    private List<Enemy> enemies = new List<Enemy>();
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        startPos = transform.position;
        SpawnEnemies();
        Enemy.OnEnemyDied += EnemyDeath;

        StartCoroutine(MoveRoutine());
        StartCoroutine(ShootRoutine());
    }

    private void OnDestroy(){
        Enemy.OnEnemyDied -= EnemyDeath;
    }

    private void SpawnEnemies(){
        enemies.Clear();
        for (int row = 0; row < rows; row++){
            GameObject prefab = enemyPrefabs[Mathf.Min(row, enemyPrefabs.Length-1)];
            for (int col = 0; col < cols; col++){
                Vector3 pos = startPos + new Vector3(col * spacingX, -row * spacingY, 0);
                GameObject obj = Instantiate(prefab, pos, Quaternion.identity, transform);
                enemies.Add(obj.GetComponent<Enemy>());
            }
        }
    }

    private IEnumerator MoveRoutine(){
        float leftEdge = -8f;
        float rightEdge = 8f;

        while (enemies.Count > 0) {
            enemies.RemoveAll(e => e == null);
            if (enemies.Count == 0) yield break;

            float squadLeft = float.MaxValue;
            float squadRight = float.MinValue;
            foreach (Enemy e in enemies){
                if (e.transform.position.x < squadLeft) squadLeft = e.transform.position.x;
                if (e.transform.position.x > squadRight) squadRight = e.transform.position.x;
            }

            bool hitEdge = (squadLeft + direction * moveStep < leftEdge) ||
                (squadRight + direction * moveStep > rightEdge);

            foreach (Enemy e in enemies){
                e.transform.position += Vector3.right * direction * moveStep;
                if (hitEdge) e.transform.position += Vector3.down * moveStep;
            }

            if (hitEdge) direction *= -1;

            float speedMultiplier = 1f + (1f - ((float)enemies.Count / (rows * cols)));
            yield return new WaitForSeconds(stepInterval / speedMultiplier);
        }
    }

    private IEnumerator ShootRoutine(){
        while (enemies.Count > 0){
            yield return new WaitForSeconds(shootInterval);
            Dictionary<int, Enemy> bottomEnemies = new Dictionary<int, Enemy>();
            
            foreach (Enemy e in enemies){
                if (e == null) continue;
                int col = Mathf.RoundToInt(e.transform.localPosition.x/spacingX);
                if (!bottomEnemies.ContainsKey(col) || e.transform.localPosition.y 
                    < bottomEnemies[col].transform.localPosition.y)
                        bottomEnemies[col] = e;
            }
            if (bottomEnemies.Count == 0) continue;

            List<Enemy> quickDraw = new List<Enemy>(bottomEnemies.Values);
            Enemy shooter = quickDraw[Random.Range(0, quickDraw.Count)];
            ShootFrom(shooter);
        }
    }

    private void ShootFrom(Enemy shooter){
        GameObject bullet = Instantiate(bulletPrefab, shooter.transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null){
            rb.linearVelocity = Vector2.down * bulletSpeed;
        }
    }
    
    private void EnemyDeath(float points){
        enemies.RemoveAll(e => e == null);
    }
}
