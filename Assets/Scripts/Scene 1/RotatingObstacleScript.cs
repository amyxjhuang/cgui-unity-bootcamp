using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 30f;

    void Start()
    {
        rotationSpeed = Random.Range(30f, 60f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationAxis = new Vector3(0,1,0);
        transform.Rotate(0f,rotationSpeed * Time.deltaTime, 0f);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if (player != null) {
                Debug.Log("Player hit obstacle");
                ScoreManagerScript.Instance.subtractScore(1);
            }
        }
    }
}
