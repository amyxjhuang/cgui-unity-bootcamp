using UnityEngine;

public class RotatorPlatformScript : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 3f;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotationSpeed = Random.Range(5f, 10f);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed, 0f, 0f) * Time.deltaTime);
        // All child objects will rotate in unison because they're part of the transform hierarchy
        // rb.angularVelocity = new Vector3(rotationSpeed * Mathf.Deg2Rad, 0f, 0f);
    }
}
