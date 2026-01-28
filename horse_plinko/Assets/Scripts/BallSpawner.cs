using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawner : MonoBehaviour
{
    public GameObject horsePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(horsePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            Transform myTransform = GetComponent<Transform>();
            Instantiate(horsePrefab, myTransform.position, Quaternion.identity);
        }
    }
}
