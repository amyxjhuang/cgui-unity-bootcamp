using UnityEngine;

public class RotatorPlatformScript : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationSpeed = Random.Range(5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate around the x-axis
        // All child objects will rotate in unison because they're part of the transform hierarchy
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }
}
