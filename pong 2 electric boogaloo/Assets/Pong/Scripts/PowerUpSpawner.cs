using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnInterval = 10f;
    public Vector3 spawnArea;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         InvokeRepeating(nameof(Spawn), 5f, spawnInterval);
    }

    // Update is called once per frame
    void Spawn()
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            0,
            Random.Range(-spawnArea.z, spawnArea.z));

        Instantiate(powerUpPrefab, randomPos, Quaternion.identity);
    }
}
