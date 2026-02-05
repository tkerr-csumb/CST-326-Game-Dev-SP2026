using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawner : MonoBehaviour
{
    public GameObject horsePrefab;
    public float spawnerDistance;
    public float spawnerSpeed;
    private Vector3 startSpot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startSpot = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float xOffset = Mathf.Sin(Time.time * spawnerSpeed * 2f * Mathf.PI) * spawnerDistance;
        transform.position = startSpot + new Vector3(xOffset, 0f, 0f);
        if(Keyboard.current.spaceKey.isPressed)
        {
            Transform myTransform = GetComponent<Transform>();
            Instantiate(horsePrefab, myTransform.position, Quaternion.identity);
        }
    }
}
