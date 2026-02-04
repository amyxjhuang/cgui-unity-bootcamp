using UnityEngine;

public class ScalingPlateScript : MonoBehaviour
{
    [SerializeField] private float scaleSpeed = 1f;
    [SerializeField] private float scaleAmount = 0.5f;
    [SerializeField] private Vector3 baseScale = Vector3.one;

    private void Start()
    {
        // Store the initial scale as the base scale
        if (baseScale == Vector3.one)
        {
            baseScale = transform.localScale;
        }
        scaleAmount = Random.Range(0.3f, 0.7f);

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the scale factor using sine wave for smooth oscillation
        float scaleFactor = 1f + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        
        transform.localScale = new Vector3(
            baseScale.x * scaleFactor,
            baseScale.y,
            baseScale.z * scaleFactor
        );
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManagerScript.Instance.subtractScore(1);
        }
    }
}
