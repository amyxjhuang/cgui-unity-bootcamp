using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 3f;
    void Start()
    {
        rotationSpeed = Random.Range(1f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null) {
                player.decreaseScore(1);
            }
        }
    }
}
